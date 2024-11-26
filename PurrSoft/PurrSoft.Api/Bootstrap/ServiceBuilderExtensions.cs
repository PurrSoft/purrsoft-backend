using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Bootstrap;
using PurrSoft.Application.Queries.AccountQueries;
using PurrSoft.Common.Config;
using PurrSoft.Domain.Repositories;
using PurrSoft.Infrastructure.Bootstrap;
using PurrSoft.Persistence;
using PurrSoft.Persistence.Bootstrap;
using PurrSoft.Persistence.Repositories;

namespace PurrSoft.Api.Bootstrap;
public static class ServiceBuilderExtensions
{
    public static void RegisterWebApiServices(this IServiceCollection services)
    {
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // register cors
        services.AddCorsPolicy();
        services.RegisterWebApiServices();
        // register dbcontext
        services.AddDbContext(configuration);
        // jwt
        IConfigurationSection jwtSettings = configuration.GetSection("JwtConfig");
        string jwtSecret = jwtSettings["secret"] ?? string.Empty;
        JwtConfig jwtConfig = new()
        {
            Audience = jwtSettings["validAudience"] ?? string.Empty,
            ExpiresIn = Convert.ToDouble(jwtSettings["expiresIn"]),
            Issuer = jwtSettings["validIssuer"] ?? string.Empty,
            Secret = jwtSettings["secret"] ?? string.Empty
        };
        services.AddJwtAuthentication(configuration, jwtSettings, jwtConfig);
        //identity
        services.ConfigureIdentity();
        //mediatr
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetLoggedInUserQuery).Assembly));
        // validators
        services.AddValidatorsFromAssembly(typeof(GetLoggedInUserQuery).Assembly);
        services.AddHttpContextAccessor();
        //register infrastructure services
        services.RegisterInfrastructureServices();
        //register application services
        services.RegisterApplicationServices();
        // register repositories
        services.AddScoped(typeof(ILogRepository<>), typeof(LogRepository<>));
        services.RegisterRepositories();
        // web api
        services.RegisterWebApiServices();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfig();
        services.AddControllers();
        // singleton for action context accessor
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        // Register JwtConfig as a singleton
        services.AddSingleton(jwtConfig);
        // scope for action context and url helper
        services.AddScoped(x =>
        {
            ActionContext actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            IUrlHelperFactory factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext);
        });

        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        services.AddDbContext<PurrSoftDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    private static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(
            options =>
            {
                options.AddPolicy("_myAllowSpecificOrigins",
                    policy =>
                        policy.WithOrigins("https://localhost:7233", "https://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
            });
    }
}
