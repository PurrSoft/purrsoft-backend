using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.ShiftCommands;

public class ShiftCommandHandler(
	IRepository<Shift> _shiftRepository,
	IRepository<Volunteer> _volunteerRepository,
	ILogRepository<ShiftCommandHandler> logRepository) :
	IRequestHandler<CreateShiftCommand, CommandResponse>,
	IRequestHandler<UpdateShiftCommand, CommandResponse>,
	IRequestHandler<DeleteShiftCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (request.ShiftDto.VolunteerId != null)
			{
				Volunteer? volunteer = await _volunteerRepository.Query(v => v.UserId == request.ShiftDto.VolunteerId)
					.FirstOrDefaultAsync(cancellationToken);
				if (volunteer == null)
				{
					return CommandResponse.Failed($"Volunteer with ID {request.ShiftDto.VolunteerId} not found.");
				}
			}

			Shift shift = new()
			{
				Start = DateTime.SpecifyKind(request.ShiftDto.Start, DateTimeKind.Utc),
				ShiftType = Enum.Parse<ShiftType>(request.ShiftDto.ShiftType),
				ShiftStatus = Enum.Parse<ShiftStatus>(request.ShiftDto.ShiftStatus),
				VolunteerId = request.ShiftDto.VolunteerId
			};

			_shiftRepository.Add(shift);
			await _shiftRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Shift? shift = _shiftRepository
				.Query(s => s.Id == request.ShiftDto.Id)
				.FirstOrDefault();
			if (shift == null)
			{
				return CommandResponse.Failed("Shift not found.");
			}

			if (request.ShiftDto.VolunteerId != null)
			{
				Volunteer? volunteer = await _volunteerRepository.Query(v => v.UserId == request.ShiftDto.VolunteerId)
					.FirstOrDefaultAsync(cancellationToken);
				if (volunteer == null)
				{
					return CommandResponse.Failed($"Volunteer with ID {request.ShiftDto.VolunteerId} not found.");
				}
			}
			request.ShiftDto.Update(shift);
			

			await _shiftRepository.SaveChangesAsync(cancellationToken);
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Shift? shift = await _shiftRepository.Query(s => s.Id.ToString() == request.Id)
				.FirstOrDefaultAsync(cancellationToken);
			if (shift == null)
			{
				return CommandResponse.Failed("Shift not found.");
			}

			_shiftRepository.Remove(shift);
			await _shiftRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}
}
