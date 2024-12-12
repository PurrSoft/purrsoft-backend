using PurrSoft.Application.Models;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.QueryOverviews.Mappers;

public static class ApplicationUserMappers
{
    public static IQueryable<ApplicationUserDto> ProjectToDto
        (this IQueryable<ApplicationUser> query)
        => query.Select(u => new ApplicationUserDto
        {
            DisplayName = u.FullName,
            Email = u.Email,
            FirstName = u.FirstName,
            Id = u.Id,
            LastName = u.LastName,
			Address = u.Address,
			Roles = u.UserRoles.Select(ur => ur.Role.Name).ToArray(),
        });
}