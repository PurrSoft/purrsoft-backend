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
	ILogRepository<ShiftCommandHandler> logRepository,
	IRepository<ApplicationUser> _userRepository,
	ICurrentUserService _currentUserService) :
	IRequestHandler<CreateShiftCommand, CommandResponse>,
	IRequestHandler<UpdateShiftCommand, CommandResponse>,
	IRequestHandler<DeleteShiftCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Shift shift = new()
			{
				Start = DateTime.SpecifyKind(request.ShiftDto.Start, DateTimeKind.Utc),
				ShiftType = Enum.Parse<ShiftType>(request.ShiftDto.ShiftType),
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

			shift.Start = request.ShiftDto.Start;
			shift.ShiftType = Enum.Parse<ShiftType>(request.ShiftDto.ShiftType);
			shift.VolunteerId = request.ShiftDto.VolunteerId;

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
