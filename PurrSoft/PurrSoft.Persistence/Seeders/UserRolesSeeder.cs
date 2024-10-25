using Microsoft.EntityFrameworkCore;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Persistence.Seeders;

public static class RolesSeeder
{
    public static void SeederForRoles(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role()
            {
                Id = "c68c49f4-7cff-4c4e-a836-f670f386c0f0",
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new Role()
            {
                Id = "5f50c0ac-fadf-4792-af6a-54dcd5c3aab3",
                Name = "Volunteer",
                NormalizedName = "VOLUNTEER"
            },
            new Role()
            {
                Id = "16f9f85a-1389-489a-87d4-cef974323047",
                Name = "Foster",
                NormalizedName = "FOSTER"
            });
    }
}