using MediatR;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PurrSoft.Application.Commands.RequestCommands;

public class RequestCommandHandler(
	IRepository<Request> _requestRepository,
	IRepository<ApplicationUser> _applicationUserRepository,
	ILogRepository<RequestCommandHandler> logRepository) :
	IRequestHandler<CreateRequestCommand, CommandResponse>,
	IRequestHandler<UpdateRequestCommand, CommandResponse>,
	IRequestHandler<DeleteRequestCommand, CommandResponse>
{
	public async Task<CommandResponse> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (request.RequestDto.UserId != null)
			{
				ApplicationUser? applicationUser = await _applicationUserRepository.Query(v => v.Id == request.RequestDto.UserId)
					.FirstOrDefaultAsync(cancellationToken);
				if (applicationUser == null)
				{
					return CommandResponse.Failed($"User with ID {request.RequestDto.UserId} not found.");
				}
			}

			Request Request = new()
			{
				CreatedAt = DateTime.SpecifyKind(request.RequestDto.CreatedAt, DateTimeKind.Utc),
				RequestType = Enum.Parse<RequestType>(request.RequestDto.RequestType),
				Description = request.RequestDto.Description,
				UserId = request.RequestDto.UserId,
				Images = request.RequestDto.Images
			};

			_requestRepository.Add(Request);
			await _requestRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Request? Request = _requestRepository
				.Query(s => s.Id == request.RequestDto.Id)
				.FirstOrDefault();
			if (Request == null)
			{
				return CommandResponse.Failed("Request not found.");
			}

			if (request.RequestDto.UserId != null)
			{
				ApplicationUser? applicationUser = await _applicationUserRepository.Query(v => v.Id == request.RequestDto.UserId)
					.FirstOrDefaultAsync(cancellationToken);
				if (applicationUser == null)
				{
					return CommandResponse.Failed($"User with ID {request.RequestDto.UserId} not found.");
				}
			}

			Request.Description = request.RequestDto.Description;
			Request.RequestType = Enum.Parse<RequestType>(request.RequestDto.RequestType);
			Request.CreatedAt = request.RequestDto.CreatedAt;
			Request.UserId = request.RequestDto.UserId;
			Request.Images = request.RequestDto.Images;

			await _requestRepository.SaveChangesAsync(cancellationToken);
			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}

	public async Task<CommandResponse> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Request? Request = await _requestRepository.Query(s => s.Id.ToString() == request.Id)
				.FirstOrDefaultAsync(cancellationToken);
			if (Request == null)
			{
				return CommandResponse.Failed("Request not found.");
			}

			_requestRepository.Remove(Request);
			await _requestRepository.SaveChangesAsync(cancellationToken);

			return CommandResponse.Ok();
		}
		catch (Exception ex)
		{
			logRepository.LogException(LogLevel.Error, ex);
			throw;
		}
	}
}