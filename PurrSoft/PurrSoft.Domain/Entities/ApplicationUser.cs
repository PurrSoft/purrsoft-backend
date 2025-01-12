using Microsoft.AspNetCore.Identity;

namespace PurrSoft.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
	public string? Address { get; set; }
	public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<Request> Requests { get; set; }

	public ApplicationUser()
    {
        UserRoles = [];
		Requests = [];
	}
}