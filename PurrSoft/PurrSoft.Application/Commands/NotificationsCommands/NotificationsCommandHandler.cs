using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrSoft.Application.Common;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Application.Commands.NotificationsCommands
{
    public class NotificationCommandHandler(
        IRepository<Notifications> notificationsRepository,
        ILogRepository<NotificationCommandHandler> logRepository,
        ISignalRService signalRService)
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

                NotificationsDto? notificationDto = new NotificationsDto
                {
                    Id = newNotification.Id,
                    Type = newNotification.Type,
                    Message = newNotification.Message,
                    IsRead = newNotification.IsRead
                };
                await signalRService.NotifyAllAsync<Notifications>(NotificationOperationType.Add, notificationDto);

                return CommandResponse.Ok(newNotification.Id);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return (CommandResponse<Guid>)CommandResponse.Failed(ex.Errors.ToList());
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return (CommandResponse<Guid>)CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Notification", "An error occurred while creating the notification.")
                });
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

                NotificationsDto? notificationDto = new NotificationsDto
                {
                    Id = notification.Id,
                    Type = notification.Type,
                    Message = notification.Message,
                    IsRead = notification.IsRead
                };
                await signalRService.NotifyAllAsync<Notifications>(NotificationOperationType.Update, notificationDto);

                return CommandResponse.Ok();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return CommandResponse.Failed(ex.Errors.ToList());
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Notification", "An error occurred while updating the notification.")
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

                await signalRService.NotifyAllAsync<Notifications>(NotificationOperationType.Delete, request.NotificationId);

                return CommandResponse.Ok();
            }
            catch (FluentValidation.ValidationException ex)
            {
                return CommandResponse.Failed(ex.Errors.ToList());
            }
            catch (Exception ex)
            {
                logRepository.LogException(LogLevel.Error, ex);
                return CommandResponse.Failed(new List<ValidationFailure>
                {
                    new("Notification", "An error occurred while deleting the notification.")
                });
            }
        }
    }
}
