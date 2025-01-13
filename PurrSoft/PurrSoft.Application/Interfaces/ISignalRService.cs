using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Interfaces;

public interface ISignalRService
{
    Task NotifyAllAsync<T>(OperationType operationType, T data);
    Task NotifyUserAsync<T>(string userId, OperationType operationType, T data);
    Task NotifyRoleAsync<T>(string role, OperationType operationType, T data);
}

