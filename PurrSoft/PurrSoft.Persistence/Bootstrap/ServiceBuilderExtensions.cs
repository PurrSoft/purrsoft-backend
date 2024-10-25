using Microsoft.Extensions.DependencyInjection;
using PurrSoft.Domain.Repositories;
using PurrSoft.Persistence.Repositories;

namespace PurrSoft.Persistence.Bootstrap;

public static class ServiceBuilderExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        // base repo
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        // and user repo
        services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
    }
}
