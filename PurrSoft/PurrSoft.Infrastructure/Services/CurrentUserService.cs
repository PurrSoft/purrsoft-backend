using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PurrSoft.Common.Identity;
using PurrSoft.Domain.Entities;
using System.Security.Claims;

namespace PurrSoft.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    : ICurrentUserService
{
    public async Task<CurrentUser?> GetCurrentUser()
    {
        if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            return null;

        ApplicationUser user = await
            userManager.
                FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        return user == null
            ? null
            : new CurrentUser()
            {
                UserId = user.Id,
            };
    }
}
