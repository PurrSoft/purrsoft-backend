using Microsoft.AspNetCore.Identity;

namespace PurrSoft.Domain.Entities;

public class Role : IdentityRole
{
    public Role() => UserRoles = [];
    public virtual ICollection<UserRole> UserRoles { get; set; }
}