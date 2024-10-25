using Microsoft.AspNetCore.Identity;
using PurrSoft.Domain.Entities;
using PurrSoft.Persistence;

namespace PurrSoft.Api.Bootstrap;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        //to do
        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, Role>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = false;
                o.User.RequireUniqueEmail = true;
                o.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<PurrSoftDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}