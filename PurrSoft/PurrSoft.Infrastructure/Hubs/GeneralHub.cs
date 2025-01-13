using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace PurrSoft.Infrastructure.Hubs;

public class GeneralHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userRoles = Context.User?.Claims
            .Where(claim => claim.Type == ClaimTypes.Role)
            .Select(claim => claim.Value)
            .ToList();
        if (userRoles != null && userRoles.Any())
        {
            foreach (var role in userRoles)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
            }
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userRoles = Context.User?.Claims
            .Where(claim => claim.Type == ClaimTypes.Role)
            .Select(claim => claim.Value)
            .ToList();
        if (userRoles != null && userRoles.Any())
        {
            foreach (var role in userRoles)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
}
