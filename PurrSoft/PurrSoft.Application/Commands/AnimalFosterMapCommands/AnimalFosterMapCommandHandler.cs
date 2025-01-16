using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews.Mappers;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;
using static PurrSoft.Application.Commands.AnimalFosterMapCommands.AnimalFosterMapCommands;

namespace PurrSoft.Application.Commands.AnimalFosterMapCommands;

public class AnimalFosterMapCommandHandler(
	IRepository<AnimalFosterMap> _animalFosterMapRepository,
	IRepository<Animal> _animalRepository,
	IRepository<Foster> _fosterRepository,
	ILogRepository<AnimalFosterMapCommandHandler> _logRepository,
	IRepository<ApplicationUser> _userRepository,
	ICurrentUserService _currentService,
    ISignalRService _signalRService
    ) :
	IRequestHandler<AddAnimalToFosterCommand, CommandResponse>,
	IRequestHandler<UpdateAnimalFosterMapCommand, CommandResponse>,
	IRequestHandler<RemoveAnimalFromFosterCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(AddAnimalToFosterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var animalExists = await isExistingAnimal(_animalRepository, request.AnimalFosterMapDto.AnimalId, cancellationToken);
			if (!animalExists)
			{
				return CommandResponse.Failed($"Animal with ID {request.AnimalFosterMapDto.AnimalId} not found.");
			}

			var fosterExists = await isExistingFoster(_fosterRepository, request.AnimalFosterMapDto.FosterId, cancellationToken);
			if (!fosterExists)
			{
				return CommandResponse.Failed($"Foster with ID {request.AnimalFosterMapDto.FosterId} not found.");
			}

			AnimalFosterMap animalFosterMap = new AnimalFosterMap
			{
				AnimalId = request.AnimalFosterMapDto.AnimalId,
				FosterId = request.AnimalFosterMapDto.FosterId,
				StartFosteringDate = DateTime.SpecifyKind(request.AnimalFosterMapDto.StartFosteringDate, DateTimeKind.Utc),
				EndFosteringDate = request.AnimalFosterMapDto.EndFosteringDate != null ?
									DateTime.SpecifyKind(request.AnimalFosterMapDto.EndFosteringDate.Value, DateTimeKind.Utc)
									: null,
				SupervisingComment = request.AnimalFosterMapDto.SupervisingComment
			};
			_animalFosterMapRepository.Add(animalFosterMap);
			await _animalFosterMapRepository.SaveChangesAsync(cancellationToken);

            AnimalFosterMapDto? animalFosterMapDto = Queryable
                .AsQueryable(new List<AnimalFosterMap> { animalFosterMap })
                .ProjectToDto()
                .FirstOrDefault();
			await _signalRService.NotifyAllAsync<AnimalFosterMap>(NotificationOperationType.Add, animalFosterMapDto);

            var resp = CommandResponse.Ok();
			return CommandResponse.Ok();
		}
		catch (DbUpdateException ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			return CommandResponse.Failed("An error occurred while saving the data. Ensure the IDs are valid.");
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
			bool animalExists = await isExistingAnimal(_animalRepository, request.AnimalFosterMapDto.AnimalId, cancellationToken); if (!animalExists)
			{
				return CommandResponse.Failed($"Animal with ID {request.AnimalFosterMapDto.AnimalId} not found.");
			}

			bool fosterExists = await isExistingFoster(_fosterRepository, request.AnimalFosterMapDto.FosterId, cancellationToken);
			if (!fosterExists)
			{
				return CommandResponse.Failed($"Foster with ID {request.AnimalFosterMapDto.FosterId} not found.");
			}

			AnimalFosterMap? animalFosterMap =
				_animalFosterMapRepository
					.Query(x => x.Id == request.AnimalFosterMapDto.Id)
					.FirstOrDefault();
			if (animalFosterMap == null)
			{
				return CommandResponse.Failed("Animal is not assigned to this foster.");
			}

			animalFosterMap.AnimalId = request.AnimalFosterMapDto.AnimalId;
			animalFosterMap.FosterId = request.AnimalFosterMapDto.FosterId;
			animalFosterMap.StartFosteringDate = DateTime.SpecifyKind(request.AnimalFosterMapDto.StartFosteringDate, DateTimeKind.Utc);
			if (request.AnimalFosterMapDto.EndFosteringDate is not null)
			{
				animalFosterMap.EndFosteringDate = DateTime.SpecifyKind(request.AnimalFosterMapDto.EndFosteringDate.Value, DateTimeKind.Utc);
			}
			animalFosterMap.SupervisingComment = request.AnimalFosterMapDto.SupervisingComment;

			await _animalFosterMapRepository.SaveChangesAsync(cancellationToken);

            AnimalFosterMapDto? animalFosterMapDto = Queryable
                .AsQueryable(new List<AnimalFosterMap> { animalFosterMap })
                .ProjectToDto()
                .FirstOrDefault();
            await _signalRService.NotifyAllAsync<AnimalFosterMap>(NotificationOperationType.Update, animalFosterMapDto);

            return CommandResponse.Ok();
		}
		catch (DbUpdateException ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			return CommandResponse.Failed("An error occurred while saving the data. Ensure the IDs are valid.");
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	private static async Task<bool> isExistingAnimal(IRepository<Animal> _animalRepository, Guid id, CancellationToken cancellationToken)
	{
		return await _animalRepository.Query(x => x.Id == id).AnyAsync(cancellationToken);
	}

	private static async Task<bool> isExistingFoster(IRepository<Foster> _fosterRepository, string id, CancellationToken cancellationToken)
	{
		return await _fosterRepository.Query(x => x.UserId == id).AnyAsync(cancellationToken);
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

			await _signalRService.NotifyAllAsync<AnimalFosterMap>(NotificationOperationType.Delete, request.AnimalFosterMapId);

            return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			_logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}
}
