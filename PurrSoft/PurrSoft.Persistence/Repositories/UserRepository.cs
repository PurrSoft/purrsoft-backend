
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Persistence.Repositories;
public class UserRepository(PurrSoftDbContext dbContext) : IUserRepository
{
    protected PurrSoftDbContext context = dbContext;
    public bool DoesUserExist(string id)
    {
        return context.Users.Any(u => u.Id == id);
    }

}