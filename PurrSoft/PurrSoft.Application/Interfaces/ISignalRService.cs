using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Interfaces;

public interface ISignalRService
{
    Task NotifyAllAsync<T>(NotificationOperationType operationType, Object? data);
    Task NotifyUserAsync<T>(string userId, NotificationOperationType operationType, Object? data);
    Task NotifyRoleAsync<T>(string role, NotificationOperationType operationType, Object? data);
}

