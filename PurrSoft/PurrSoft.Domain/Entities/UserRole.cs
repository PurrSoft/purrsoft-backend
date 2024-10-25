using Microsoft.AspNetCore.Identity;

namespace PurrSoft.Domain.Entities;

public class UserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; }
    public virtual Role Role { get; set; }
}