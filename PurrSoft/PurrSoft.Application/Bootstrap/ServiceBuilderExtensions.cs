using Microsoft.Extensions.DependencyInjection;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Services;

namespace PurrSoft.Application.Bootstrap;

public static class ServiceBuilderExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

    }
}

