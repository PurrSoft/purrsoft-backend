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
        Env.Load();

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
        // Google Credentials
        IConfigurationSection googleCredentialsSettings = configuration.GetSection("GoogleCredentialsConfig");
        GoogleCredentialsConfig googleCredentialsConfig = new()
        {
            Type = googleCredentialsSettings["type"] ?? string.Empty,
            ProjectId = googleCredentialsSettings["project_id"] ?? string.Empty,
            PrivateKeyId = Environment.GetEnvironmentVariable("GOOGLE_PRIVATE_KEY_ID") ?? string.Empty,
            PrivateKey = Environment.GetEnvironmentVariable("GOOGLE_PRIVATE_KEY") ?? string.Empty,
            ClientEmail = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_EMAIL") ?? string.Empty,
            ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? string.Empty,
            AuthUri = googleCredentialsSettings["auth_uri"] ?? string.Empty,
            TokenUri = Environment.GetEnvironmentVariable("GOOGLE_TOKEN_URI") ?? string.Empty,
            AuthProviderX509CertUrl = Environment.GetEnvironmentVariable("GOOGLE_AUTH_PROVIDER_X509_CERT_URL") ?? string.Empty,
            ClientX509CertUrl = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_X509_CERT_URL") ?? string.Empty,
            UniverseDomain = googleCredentialsSettings["universe_domain"] ?? string.Empty
        };
        if (string.IsNullOrEmpty(googleCredentialsConfig.Type) || string.IsNullOrEmpty(googleCredentialsConfig.ProjectId) || string.IsNullOrEmpty(googleCredentialsConfig.PrivateKeyId) || string.IsNullOrEmpty(googleCredentialsConfig.PrivateKey) || string.IsNullOrEmpty(googleCredentialsConfig.ClientEmail) || string.IsNullOrEmpty(googleCredentialsConfig.ClientId) || string.IsNullOrEmpty(googleCredentialsConfig.AuthUri) || string.IsNullOrEmpty(googleCredentialsConfig.TokenUri) || string.IsNullOrEmpty(googleCredentialsConfig.AuthProviderX509CertUrl) || string.IsNullOrEmpty(googleCredentialsConfig.ClientX509CertUrl) || string.IsNullOrEmpty(googleCredentialsConfig.UniverseDomain))
        {
            throw new InvalidOperationException("Invalid Google Credentials configuration.");
        }
        services.AddSingleton(googleCredentialsConfig);
        // Google Sheets Api
        IConfigurationSection googleSheetsApiSettings = configuration.GetSection("GoogleSheetsApiConfig");
        GoogleSheetsApiConfig googleSheetsApiConfig = new()
        {
            ApplicationName = googleSheetsApiSettings["application_name"] ?? string.Empty,
            SpreadsheetId = googleSheetsApiSettings["spreadsheet_id"] ?? string.Empty,
            SheetName = googleSheetsApiSettings["sheet_name"] ?? string.Empty
        };
        if (string.IsNullOrEmpty(googleSheetsApiConfig.ApplicationName) || string.IsNullOrEmpty(googleSheetsApiConfig.SpreadsheetId) || string.IsNullOrEmpty(googleSheetsApiConfig.SheetName))
        {
            throw new InvalidOperationException("Invalid Google Sheets Api configuration.");
        }
        services.AddSingleton(googleSheetsApiConfig);
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
        services.AddTransient<IGoogleSheetsService, GoogleSheetsService>();
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
