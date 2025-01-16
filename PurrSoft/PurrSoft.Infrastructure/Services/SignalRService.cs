using Microsoft.AspNetCore.SignalR;
using PurrSoft.Application.Interfaces;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Infrastructure.Hubs;

namespace PurrSoft.Infrastructure.Services;

public class SignalRService : ISignalRService
{
    private readonly IHubContext<GeneralHub> _hubContext;

    public SignalRService(IHubContext<GeneralHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyAllAsync<T>(NotificationOperationType operationType, Object? data)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", new
        {
            entityType = typeof(T).Name,
            operationType = operationType.ToString(),
            payload = data
        });
    }

    public async Task NotifyRoleAsync<T>(string role, NotificationOperationType operationType, Object? data)
    {
        await _hubContext.Clients.Group(role).SendAsync("ReceiveMessage", new
        {
            entityType = typeof(T).Name,
            operationType = operationType.ToString(),
            payload = data
        });
    }

    public async Task NotifyUserAsync<T>(string userId, NotificationOperationType operationType, Object? data)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", new
        {
            entityType = typeof(T).Name,
            operationType = operationType.ToString(),
            payload = data
        });
    }
}
