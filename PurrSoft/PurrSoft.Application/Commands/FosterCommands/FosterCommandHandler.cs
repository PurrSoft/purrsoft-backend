using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;
using static PurrSoft.Application.Commands.FosterCommands.FosterCommands;

namespace PurrSoft.Application.Commands.FosterCommands;

public class FosterCommandHandler(
	IRepository<Foster> _fosterRepository,
	ILogRepository<FosterCommandHandler> _logRepository,
	IRepository<ApplicationUser> _userRepository,
	ICurrentUserService _currentService) :
	IRequestHandler<CreateFosterCommand, CommandResponse>,
	IRequestHandler<UpdateFosterCommand, CommandResponse>,
	IRequestHandler<DeleteFosterCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(CreateFosterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Foster foster = new Foster
			{
				UserId = request.FosterDto.UserId,
				StartDate = request.FosterDto.StartDate,
				EndDate = null,
				Status = Enum.Parse<FosterStatus>(request.FosterDto.Status),
				Location = request.FosterDto.Location,
				MaxAnimalsAllowed = request.FosterDto.MaxAnimalsAllowed,
				HomeDescription = request.FosterDto.HomeDescription,
				ExperienceLevel = request.FosterDto.ExperienceLevel,
				HasOtherAnimals = request.FosterDto.HasOtherAnimals,
				OtherAnimalDetails = request.FosterDto.OtherAnimalDetails,
				AnimalFosteredCount = request.FosterDto.AnimalFosteredCount,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			_fosterRepository.Add(foster);
			await _fosterRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(UpdateFosterCommand request, CancellationToken cancellationToken)
	{
		CurrentUser currentUser = await _currentService.GetCurrentUser();

		if (currentUser == null)
		{
			throw new UnauthorizedAccessException();
		}

		ApplicationUser? user = await _userRepository
			.Query(u => u.Id == currentUser.UserId)
			.Include(u => u.UserRoles)
			.ThenInclude(ur => ur.Role)
			.FirstOrDefaultAsync(cancellationToken);

		// Check if user is a manager or foster, if it's a foster we need to check if the foster is the same as the one being updated
		if (user == null
			|| !user.UserRoles.Any(ur => ur.Role.Name == "Manager" || (ur.Role.Name == "Foster" && user.Id == request.FosterDto.UserId)))
		{
			throw new UnauthorizedAccessException();
		}

		try
		{
			Foster? foster = _fosterRepository.Query(f => f.UserId == request.FosterDto.UserId).FirstOrDefault();
			if (foster == null)
			{
				return CommandResponse.Failed(new[] { "There is no Foster with that id!" });
			}

			foster.UserId = request.FosterDto.UserId;
			foster.StartDate = request.FosterDto.StartDate;
			foster.EndDate = request.FosterDto.EndDate;
			foster.Status = Enum.Parse<FosterStatus>(request.FosterDto.Status);
			foster.Location = request.FosterDto.Location;
			foster.MaxAnimalsAllowed = request.FosterDto.MaxAnimalsAllowed;
			foster.HomeDescription = request.FosterDto.HomeDescription;
			foster.ExperienceLevel = request.FosterDto.ExperienceLevel;
			foster.HasOtherAnimals = request.FosterDto.HasOtherAnimals; // if the value is omitted from the request, it will be false
			foster.OtherAnimalDetails = request.FosterDto.OtherAnimalDetails;
			foster.AnimalFosteredCount = request.FosterDto.AnimalFosteredCount;
			foster.UpdatedAt = DateTime.UtcNow;

			await _fosterRepository.SaveChangesAsync(cancellationToken);
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(DeleteFosterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Foster foster = await _fosterRepository.Query(f => f.UserId == request.Id)
				.FirstOrDefaultAsync(cancellationToken);
			if (foster == null)
			{
				return CommandResponse.Failed(new[] { "There is no Foster with that id!" });
			}

			_fosterRepository.Remove(foster);
			await _fosterRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}
}
