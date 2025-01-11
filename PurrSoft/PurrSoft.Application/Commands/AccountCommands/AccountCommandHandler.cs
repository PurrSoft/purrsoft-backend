using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.AccountCommands;

public class AccountCommandHandler(
	IRepository<ApplicationUser> _userRepository,
	IRepository<Role> _roleRepository,
	ICurrentUserService _currentService,
	ILogRepository<ApplicationUser> _logRepository) :
	IRequestHandler<UpdateAccountCommand, CommandResponse>,
	IRequestHandler<UpdateUserRolesCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
	{
		// Fetch the current user
		var currentUser = await _currentService.GetCurrentUser();
		if (currentUser == null)
		{
			throw new UnauthorizedAccessException();
		}

		// Fetch the requested user and the current user with roles
		var requestUser = await GetUserWithRoles(request.ApplicationUserDto.Id, cancellationToken);
		var user = await GetUserWithRoles(currentUser.UserId, cancellationToken);

		if (user == null)
		{
			return CommandResponse.Failed(new[] { "Current User not found" });
		}

		if (requestUser == null)
		{
			return CommandResponse.Failed(new[] { "Requested User not found" });
		}

		// Authorization check
		if (!IsAuthorizedToUpdate(user, requestUser))
		{
			throw new UnauthorizedAccessException("You are not authorized to update the requested user.");
		}

		try
		{
			// Update the user's details
			UpdateUserDetails(requestUser, request.ApplicationUserDto);

			// Save changes to the repository
			await _userRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	private async Task<ApplicationUser?> GetUserWithRoles(string userId, CancellationToken cancellationToken)
	{
		return await _userRepository
			.Query(u => u.Id == userId)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(cancellationToken);
	}

	private bool IsAuthorizedToUpdate(ApplicationUser user, ApplicationUser requestUser)
	{
		bool userIsManager = user.UserRoles.Any(ur => ur.Role.Name == "Manager");
		bool requestUserIsManager = requestUser.UserRoles.Any(ur => ur.Role.Name == "Manager");

		if (requestUser.Id == user.Id)
		{
			return true; // A user can always update their own account
		}

		if (userIsManager && !requestUserIsManager)
		{
			return true; // A manager can update non-manager accounts
		}

		// Unauthorized if both are managers or if the user is not a manager
		return false;
	}

	private void UpdateUserDetails(ApplicationUser user, ApplicationUserDto dto)
	{
		user.FirstName = dto.FirstName;
		user.LastName = dto.LastName;
		user.Email = dto.Email;
		user.Address = dto.Address;
	}

	public async Task<CommandResponse> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
	{
		// Authorization check
		var currentUser = await _currentService.GetCurrentUser();
		if (currentUser == null)
		{
			throw new UnauthorizedAccessException();
		}
		var currentUserRoles = await GetUserWithRoles(currentUser.UserId, cancellationToken);
		if (!currentUserRoles.UserRoles.Any(ur => ur.Role.Name == "Manager")) // Only managers can update roles
		{
			throw new UnauthorizedAccessException();
		}

		var user = await _userRepository
			.Query(u => u.Id == request.Id)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(cancellationToken);

		if (user == null)
		{
			return CommandResponse.Failed(new[] { "User not found" });
		}

		bool isRemovingOwnManagerRole = currentUser.UserId == user.Id && request.Roles.All(r => r != "Manager");
		bool isRemovingManagerRoleFromAnotherManager = currentUser.UserId != user.Id && user.UserRoles.Any(ur => ur.Role.Name == "Manager") && request.Roles.All(r => r != "Manager");

		if (isRemovingOwnManagerRole || isRemovingManagerRoleFromAnotherManager)
		{
			throw new UnauthorizedAccessException("Managers cannot remove their own Manager role or remove the Manager role from other managers.");
		}

		// verify the roles are all valid
		foreach (var role in request.Roles)
		{
			if (!_roleRepository.Query(r => r.Name == role).Any())
			{
				return CommandResponse.Failed(new[] { $"Role '{role}' does not exist" });
			}
		}

		// remove duplicates
		request.Roles = request.Roles.Distinct().ToArray();

		var currentRoles = user.UserRoles.Select(ur => ur.Role.Name).ToArray();
		var rolesToAdd = request.Roles.Except(currentRoles).ToArray();
		var rolesToRemove = currentRoles.Except(request.Roles).ToArray();

		user.UserRoles = user.UserRoles
			.Where(ur => !rolesToRemove.Contains(ur.Role.Name))
			.ToList();

		foreach (var role in rolesToAdd)
		{
			var userRole = new UserRole
			{
				UserId = user.Id,
				RoleId = _roleRepository.Query(r => r.Name == role).First().Id
			};
			user.UserRoles.Add(userRole);
		}

		await _userRepository.SaveChangesAsync(cancellationToken);
		return CommandResponse.Ok();
	}
}
