using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PurrSoft.Common.Config;
using PurrSoft.Domain.Entities;
using PurrSoft.Persistence;

namespace PurrSoft.Api.Bootstrap;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection jwtSettings = configuration.GetSection("JwtConfig");
        string jwtSecret = jwtSettings["secret"] ?? string.Empty;

        JwtConfig jwtConfig = new()
        {
            Audience = jwtSettings["validAudience"] ?? string.Empty,
            ExpiresIn = Convert.ToDouble(jwtSettings["expiresIn"]),
            Issuer = jwtSettings["validIssuer"] ?? string.Empty,
            Secret = jwtSettings["secret"] ?? string.Empty
        };
        services
             .AddAuthentication(opt =>
             {
                 opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             })
             .AddJwtBearer(opt =>
             {
                 opt.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["validIssuer"],
                     ValidAudience = jwtSettings["validAudience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                 };
             }).AddCookie();
        services.AddSingleton(jwtConfig);
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