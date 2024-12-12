using MediatR;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Queries.AccountQueries;

public class AccountQueryHandler
	(IRepository<ApplicationUser> userRepository,
	IRepository<Volunteer> volunteerRepository,
	IRepository<Foster> fosterRepository
	) :
	IRequestHandler<GetLoggedInUserQuery, ApplicationUserDto>,
	IRequestHandler<GetUsersByRoleQuery, CollectionResponse<ApplicationUserDto>>,
	IRequestHandler<GetRolesAndStatusesByUserIdQuery, CollectionResponse<UserRoleStatusDto>>,
	IRequestHandler<GetRolesAndDatesByUserIdQuery, CollectionResponse<UserRoleDatesDto>>
{
	public async Task<ApplicationUserDto> Handle(GetLoggedInUserQuery request,
		CancellationToken cancellationToken)
	{
		return await userRepository
			.Query(u => u.Id == request.User.UserId)
			.ProjectToDto()
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<CollectionResponse<ApplicationUserDto>>
		Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
	{
		List<ApplicationUserDto> UserList = await userRepository
			.Query(u => u.UserRoles
				.Any(role => role.Role.NormalizedName == request.Role))
			.ProjectToDto().ToListAsync(cancellationToken);
		return new CollectionResponse<ApplicationUserDto>(UserList, UserList.Count);
	}

	public async Task<CollectionResponse<UserRoleStatusDto>> Handle(GetRolesAndStatusesByUserIdQuery request, CancellationToken cancellationToken)
	{
		// for every role in the user's roles, get the status
		// return a list of UserRoleStatusDto

		var userWithRoles = await GetUserWithRoles(request.Id, cancellationToken);

		if (userWithRoles == null)
		{
			return new CollectionResponse<UserRoleStatusDto>([], 0);
		}

		var volunteerStatus = volunteerRepository.Query(v => v.UserId == request.Id).FirstOrDefault()?.Status.ToString();
		var fosterStatus = fosterRepository.Query(f => f.UserId == request.Id).FirstOrDefault()?.Status.ToString();

		var userRoleStatusDtos = userWithRoles.UserRoles.Select(ur => new UserRoleStatusDto
		{
			Role = ur.Role.Name,
			Status = ur.Role.Name switch
			{
				"Volunteer" => volunteerStatus,
				"Foster" => fosterStatus,
				_ => null,
			}
		}).ToList();

		return new CollectionResponse<UserRoleStatusDto>(userRoleStatusDtos, userRoleStatusDtos.Count);
	}

	public async Task<CollectionResponse<UserRoleDatesDto>> Handle(GetRolesAndDatesByUserIdQuery request, CancellationToken cancellationToken)
	{
		var userWithRoles = await GetUserWithRoles(request.Id, cancellationToken);

		if (userWithRoles == null)
		{
			return new CollectionResponse<UserRoleDatesDto>([], 0);
		}

		var roleDates = await GetRoleDates(request.Id, cancellationToken);

		List<UserRoleDatesDto> userRoleDatesDtos = userWithRoles.UserRoles.Select(ur => new UserRoleDatesDto
		{
			Role = ur.Role.Name,
			StartDate = roleDates.ContainsKey(ur.Role.Name) ? roleDates[ur.Role.Name].StartDate : null,
			EndDate = roleDates.ContainsKey(ur.Role.Name) ? roleDates[ur.Role.Name].EndDate : null
		}).ToList();

		return new CollectionResponse<UserRoleDatesDto>(userRoleDatesDtos, userRoleDatesDtos.Count);
	}

	private async Task<ApplicationUser?> GetUserWithRoles(string userId, CancellationToken cancellationToken)
	{
		return await userRepository
			.Query(u => u.Id == userId)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(cancellationToken);
	}

	private async Task<Dictionary<string, (DateTime? StartDate, DateTime? EndDate)>> GetRoleDates(string userId, CancellationToken cancellationToken)
	{
		var volunteer = await volunteerRepository.Query(v => v.UserId == userId).FirstOrDefaultAsync(cancellationToken);
		var foster = await fosterRepository.Query(f => f.UserId == userId).FirstOrDefaultAsync(cancellationToken);

		var roleDates = new Dictionary<string, (DateTime? StartDate, DateTime? EndDate)>
		{
			{ "Volunteer", (volunteer?.StartDate, volunteer?.EndDate) },
			{ "Foster", (foster?.StartDate, foster?.EndDate) }
		};

		return roleDates;
	}

}