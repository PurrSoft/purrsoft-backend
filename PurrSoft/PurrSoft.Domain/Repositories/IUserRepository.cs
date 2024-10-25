namespace PurrSoft.Domain.Repositories;

public interface IUserRepository
{
    bool DoesUserExist(string id);
}