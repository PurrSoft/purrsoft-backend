namespace PurrSoft.Common.Identity;

public interface ICurrentUserService
{
    Task<CurrentUser> GetCurrentUser();
}