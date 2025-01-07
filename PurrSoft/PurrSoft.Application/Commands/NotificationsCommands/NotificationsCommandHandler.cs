using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.NotificationsCommands
{
    public class NotificationCommandHandler(
        IRepository<Notifications> notificationsRepository,
        ILogRepository<NotificationCommandHandler> logRepository)
        : IRequestHandler<NotificationCommands.NotificationCommands.NotificationCreateCommand, CommandResponse<Guid>>,
          IRequestHandler<NotificationCommands.NotificationCommands.NotificationUpdateCommand, CommandResponse>,
          IRequestHandler<NotificationCommands.NotificationCommands.NotificationDeleteCommand, CommandResponse>
    {
        public async Task<CommandResponse<Guid>> Handle(NotificationCommands.NotificationCommands.NotificationCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newNotification = new Notifications
                {
                    UserId = request.UserId,
                    Type = request.Type,
                    Message = request.Message,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                notificationsRepository.Add(newNotification);
                await notificationsRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok(newNotification.Id);
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                throw;
            }
        }

        public async Task<CommandResponse> Handle(NotificationCommands.NotificationCommands.NotificationUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await notificationsRepository
                    .Query(x => x.Id == request.NotificationId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (notification == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("NotificationId", "Notification not found.")
                    });
                }

                notification.Type = request.Type ?? notification.Type;
                notification.Message = request.Message ?? notification.Message;
                if (request.IsRead.HasValue)
                {
                    notification.IsRead = request.IsRead.Value;
                }

                await notificationsRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Notification", "Failed to update notification.")
                });
            }
        }

        public async Task<CommandResponse> Handle(NotificationCommands.NotificationCommands.NotificationDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await notificationsRepository
                    .Query(x => x.Id == request.NotificationId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (notification == null)
                {
                    return CommandResponse.Failed(new List<ValidationFailure>
                    {
                        new("NotificationId", "Notification not found.")
                    });
                }

                notificationsRepository.Remove(notification);
                await notificationsRepository.SaveChangesAsync(cancellationToken);

                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Notification", "Failed to delete notification.")
                });
            }
        }
    }
}
