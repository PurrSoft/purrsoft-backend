using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PurrSoft.Application.Interfaces;
using PurrSoft.Common.Identity;
using PurrSoft.Infrastructure.RequestBehaviors;
using PurrSoft.Infrastructure.Services;

namespace PurrSoft.Infrastructure.Bootstrap;

public static class ServiceBuilderExtensions
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        //current user interface
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CurrentUserBehavior<,>));
        //validation behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        //add current user service
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        //add signalr service
        services.AddScoped<ISignalRService, SignalRService>();
    }
}