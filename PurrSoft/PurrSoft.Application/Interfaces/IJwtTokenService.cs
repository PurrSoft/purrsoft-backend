using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Interfaces;
public interface IJwtTokenService
{
    string CreateToken(ApplicationUser user, string[] roles);
}
