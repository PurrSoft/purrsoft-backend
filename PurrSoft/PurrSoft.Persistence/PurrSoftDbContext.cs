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
    public DbSet<Foster> Fosters { get; set; }
    public DbSet<Animal> Animals { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		ConfigureUser(modelBuilder);
		ConfigureRole(modelBuilder);
		//ConfigureUserRole(modelBuilder);
		ConfigureFosters(modelBuilder);
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

	private static void ConfigureFosters(ModelBuilder builder)
	{
		builder.Entity<Foster>().HasKey(f => f.UserId);
		builder.Entity<Foster>().Property(f => f.UserId).ValueGeneratedNever();
		builder.Entity<Foster>()
			.HasOne(f => f.User)
			.WithOne()
			.HasForeignKey<Foster>(f => f.UserId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);
	}
}