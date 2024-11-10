using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Domain.Entities;
using PurrSoft.Persistence.Seeders;

namespace PurrSoft.Persistence;

public class PurrSoftDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>
    , IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    public DbSet<ApplicationLog> ApplicationLogs { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalProfile> AnimalProfiles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureAnimalProfile(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureRole(modelBuilder);
        //ConfigureUserRole(modelBuilder);
        modelBuilder.SeederForRoles();
    }

    private static void ConfigureUser(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().Property(u => u.Id).ValueGeneratedNever();
        builder.Entity<ApplicationUser>().HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId);
    }

    private static void ConfigureRole(ModelBuilder builder)
    {
        builder.Entity<Role>().Property(r => r.Id).ValueGeneratedNever();
        builder.Entity<Role>().HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId);
    }

    private static void ConfigureAnimalProfile(ModelBuilder builder)
    {
        builder.Entity<AnimalProfile>()
            .HasKey(ap => ap.Id);  

        builder.Entity<Animal>()
            .HasOne(a => a.AnimalProfile)
            .WithOne(ap => ap.Animal)
            .HasForeignKey<AnimalProfile>(ap => ap.AnimalId)  // Ensure Foreign Key is AnimalId in AnimalProfile
            .OnDelete(DeleteBehavior.Cascade);
    }
}