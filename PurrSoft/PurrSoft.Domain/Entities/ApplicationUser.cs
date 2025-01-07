using Microsoft.AspNetCore.Identity;

namespace PurrSoft.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public virtual ICollection<UserRole> UserRoles { get; set; }
    
    public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public ApplicationUser()
    {
        UserRoles = [];
    }
}
