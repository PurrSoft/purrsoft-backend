using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Application.Bootstrap;
using PurrSoft.Application.Queries.AccountQueries;
using PurrSoft.Infrastructure.Bootstrap;
using PurrSoft.Persistence;
using PurrSoft.Persistence.Bootstrap;

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
        //services.AddJwtAuthentication(configuration); <- to do
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
        // regirer repositories
        services.RegisterRepositories();
        // web api
        services.RegisterWebApiServices();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        // singleton for action context accessor
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        // there will be a singleton for the jwt config
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
                        policy.WithOrigins("https://localhost:7233")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
            });
    }
}
