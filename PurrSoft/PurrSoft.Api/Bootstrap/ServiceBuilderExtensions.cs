using DotNetEnv;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Bootstrap;
using PurrSoft.Application.Interfaces;
using PurrSoft.Application.Queries.AccountQueries;
using PurrSoft.Common.Config;
using PurrSoft.Domain.Repositories;
using PurrSoft.Infrastructure.Bootstrap;
using PurrSoft.Infrastructure.Services;
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
        // Load environment variables from .env file
        Env.Load();

        // Register CORS
        services.AddCorsPolicy();
        services.RegisterWebApiServices();

        // Register DbContext
        services.AddDbContext(configuration);

        // Configure JWT
        IConfigurationSection jwtSettings = configuration.GetSection("JwtConfig");
        JwtConfig jwtConfig = new()
        {
            Audience = jwtSettings["validAudience"] ?? string.Empty,
            ExpiresIn = Convert.ToDouble(jwtSettings["expiresIn"] ?? "0"),
            Issuer = jwtSettings["validIssuer"] ?? string.Empty,
            Secret = jwtSettings["secret"] ?? string.Empty
        };

        services.AddJwtAuthentication(configuration, jwtSettings, jwtConfig);
        services.AddSingleton(jwtConfig);

        // Configure SMTP
        IConfigurationSection smtpSettings = configuration.GetSection("SmtpConfig");
        SmtpClientConfig smtpConfig = new()
        {
            Host = smtpSettings["Host"] ?? string.Empty,
            Port = Convert.ToInt32(smtpSettings["Port"] ?? "0"),
            Username = smtpSettings["Username"] ?? string.Empty,
            Password = Environment.GetEnvironmentVariable("PASSWORD_EMAIL") ?? string.Empty
        };

        if (string.IsNullOrEmpty(smtpConfig.Host) || smtpConfig.Port == 0 ||
            string.IsNullOrEmpty(smtpConfig.Username) || string.IsNullOrEmpty(smtpConfig.Password))
        {
            throw new InvalidOperationException("Invalid SMTP configuration.");
        }

        services.AddSingleton(smtpConfig);

        // Configure Identity
        services.ConfigureIdentity();

        // Register MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetLoggedInUserQuery).Assembly));

        // Register Validators
        services.AddValidatorsFromAssembly(typeof(GetLoggedInUserQuery).Assembly);
        services.AddHttpContextAccessor();

        // Register Infrastructure and Application Services
        services.RegisterInfrastructureServices();
        services.RegisterApplicationServices();

        // Register Repositories
        services.AddScoped(typeof(ILogRepository<>), typeof(LogRepository<>));
        services.RegisterRepositories();

        // Register Swagger and Controllers
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfig();
        services.AddControllers();

        // Singleton for Action Context Accessor
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // Register Email Service
        services.AddTransient<IEmailService, EmailService>();

        // Register URL Helper
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
        services.AddCors(options =>
        {
            options.AddPolicy("_myAllowSpecificOrigins", policy =>
                policy.WithOrigins("https://localhost:7233", "https://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());
        });
    }
}
