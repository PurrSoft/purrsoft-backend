using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;
using static PurrSoft.Application.Commands.AnimalFosterMapCommands.AnimalFosterMapCommands;

namespace PurrSoft.Application.Commands.AnimalFosterMapCommands;

public class AnimalFosterMapCommandHandler(
	IAnimalFosterMapRepository _animalFosterMapRepository,
	ILogRepository<AnimalFosterMapCommandHandler> _logRepository,
	IRepository<ApplicationUser> _userRepository,
	ICurrentUserService _currentService
	) :
	IRequestHandler<AddAnimalToFosterCommand, CommandResponse>,
	IRequestHandler<UpdateAnimalFosterMapCommand, CommandResponse>,
	IRequestHandler<RemoveAnimalFromFosterCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(AddAnimalToFosterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			AnimalFosterMap animalFosterMap = new AnimalFosterMap
			{
				AnimalId = request.AnimalFosterMapDto.AnimalId,
				FosterId = request.AnimalFosterMapDto.FosterId,
				StartFosteringDate = request.AnimalFosterMapDto.StartFosteringDate.ToUniversalTime(),
				EndFosteringDate = request.AnimalFosterMapDto.EndFosteringDate?.ToUniversalTime(),
				SupervisingComment = request.AnimalFosterMapDto.SupervisingComment
			};
			_animalFosterMapRepository.Add(animalFosterMap);
			await _animalFosterMapRepository.SaveChangesAsync(cancellationToken);
			var resp = CommandResponse.Ok();
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(UpdateAnimalFosterMapCommand request, CancellationToken cancellationToken)
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
			|| !user.UserRoles.Any(ur => ur.Role.Name == "Manager" || (ur.Role.Name == "Foster" &&
				user.Id == request.AnimalFosterMapDto.FosterId)))
		{
			throw new UnauthorizedAccessException();
		}

		try
		{
			AnimalFosterMap? animalFosterMap =
				_animalFosterMapRepository.Query(x => x.Id == request.AnimalFosterMapDto.Id
				&& x.AnimalId == request.AnimalFosterMapDto.AnimalId
				&& x.FosterId == request.AnimalFosterMapDto.FosterId).FirstOrDefault();
			if (animalFosterMap == null)
			{
				return CommandResponse.Failed("Animal is not assigned to this foster.");
			}

			animalFosterMap.StartFosteringDate = request.AnimalFosterMapDto.StartFosteringDate.ToUniversalTime();
			animalFosterMap.EndFosteringDate = request.AnimalFosterMapDto.EndFosteringDate?.ToUniversalTime();
			animalFosterMap.SupervisingComment = request.AnimalFosterMapDto.SupervisingComment;

			await _animalFosterMapRepository.SaveChangesAsync(cancellationToken);
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(RemoveAnimalFromFosterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			AnimalFosterMap? animalFosterMap = _animalFosterMapRepository.
				Query(x => x.Id.ToString() == request.AnimalFosterMapId).FirstOrDefault();
			if (animalFosterMap == null)
			{
				return CommandResponse.Failed("Fostering not found.");
			}

			_animalFosterMapRepository.Remove(animalFosterMap);
			await _animalFosterMapRepository.SaveChangesAsync(cancellationToken);
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}
}
