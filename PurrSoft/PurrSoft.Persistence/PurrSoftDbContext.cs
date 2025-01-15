using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;
using PurrSoft.Persistence.Seeders;

namespace PurrSoft.Persistence;

public class PurrSoftDbContext(DbContextOptions options)
	: IdentityDbContext<ApplicationUser, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{

	public DbSet<ApplicationLog> ApplicationLogs { get; set; }
	public DbSet<Foster> Fosters { get; set; }
	public DbSet<Animal> Animals { get; set; }
	public DbSet<Volunteer> Volunteers { get; set; }
	public DbSet<AnimalFosterMap> AnimalFosters { get; set; }
	public DbSet<AnimalProfile> AnimalProfiles { get; set; }
	public DbSet<Shift> Shifts { get; set; }
	public DbSet<Request> Requests { get; set; }
	public DbSet<LeaveRequest> LeaveRequests { get; set; }
	public DbSet<Notifications> Notifications { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		ConfigureUser(modelBuilder);
		ConfigureRole(modelBuilder);
		//ConfigureUserRole(modelBuilder);
		ConfigureFosters(modelBuilder);
		ConfigureVolunteers(modelBuilder);
		ConfigureShifts(modelBuilder);
		ConfigureAnimalProfile(modelBuilder);
		ConfigureFosterAnimals(modelBuilder);
		ConfigureRequests(modelBuilder);
		ConfigureNotifications(modelBuilder);

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

	private static void ConfigureShifts(ModelBuilder builder)
	{
		builder.Entity<Shift>().HasKey(s => s.Id);
		builder.Entity<Shift>()
			.HasOne(s => s.Volunteer)
			.WithMany(v => v.Shifts)
			.HasForeignKey(s => s.VolunteerId)
			.OnDelete(DeleteBehavior.Restrict);
	}


	private static void ConfigureVolunteers(ModelBuilder builder)
	{
		builder.Entity<Volunteer>().HasKey(v => v.UserId);
		builder.Entity<Volunteer>()
			.HasOne(v => v.User)
			.WithOne()
			.HasForeignKey<Volunteer>(v => v.UserId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);
		builder.Entity<Volunteer>()
			.HasOne(v => v.Supervisor)
			.WithMany()
			.OnDelete(DeleteBehavior.Restrict);
		builder.Entity<Volunteer>()
			.HasMany(v => v.Trainers)
			.WithMany()
			.UsingEntity<Dictionary<string, object>>(
				"VolunteerTrainer",
				j => j.HasOne<ApplicationUser>()
					  .WithMany()
					  .HasForeignKey("TrainerId")
					  .OnDelete(DeleteBehavior.Restrict),
				j => j.HasOne<Volunteer>()
					  .WithMany()
					  .HasForeignKey("VolunteerId")
					  .OnDelete(DeleteBehavior.Cascade)
			);
	}

	private static void ConfigureFosterAnimals(ModelBuilder builder)
	{
		builder.Entity<AnimalFosterMap>().HasKey(af => af.Id);
		builder.Entity<AnimalFosterMap>()
			.HasOne(af => af.Animal)
			.WithMany(a => a.FosteredBy)
			.HasForeignKey(af => af.AnimalId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);


		builder.Entity<AnimalFosterMap>()
			.HasOne(af => af.Foster)
			.WithMany(f => f.FosteredAnimals)
			.HasForeignKey(af => af.FosterId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);
	}

	private static void ConfigureAnimalProfile(ModelBuilder builder)
	{
		builder.Entity<AnimalProfile>(entity =>
		{
			// Configure the primary key to use AnimalId
			entity.HasKey(ap => ap.AnimalId);

			// Configure one-to-one relationship between Animal and AnimalProfile
			entity.HasOne(ap => ap.Animal)
				.WithOne(a => a.AnimalProfile) // Define the one-to-one relationship
				.HasForeignKey<AnimalProfile>(ap => ap.AnimalId) // Ensure AnimalId is used as the FK
				.OnDelete(DeleteBehavior.Cascade); // Cascade delete when Animal is deleted

			// Configure JSON column for UsefulLinks (if applicable)
			entity.Property(ap => ap.UsefulLinks)
				.HasColumnType("jsonb");
		});
	}

	private static void ConfigureRequests(ModelBuilder builder)
	{
		builder.Entity<Request>().HasKey(r => r.Id);
		builder.Entity<Request>()
			.HasOne(r => r.User)
			.WithMany(u => u.Requests)
			.HasForeignKey(r => r.UserId)
			.IsRequired();
		builder.Entity<Request>()
			.HasDiscriminator<RequestType>(nameof(RequestType))
			.HasValue<Request>(RequestType.Consultation)
			.HasValue<LeaveRequest>(RequestType.Leave)
			.HasValue<Request>(RequestType.Refill)
			.HasValue<Request>(RequestType.Supplies);
	}
	private static void ConfigureNotifications(ModelBuilder builder)
	{
		builder.Entity<Notifications>()
			.HasKey(n => n.Id); // Primary key

		builder.Entity<Notifications>()
			.HasOne(n => n.User) // Navigation property to ApplicationUser
			.WithMany(u => u.Notifications) // Specify collection navigation in ApplicationUser
			.HasForeignKey(n => n.UserId) // Foreign key in Notifications
			.IsRequired() // UserId is required
			.OnDelete(DeleteBehavior.Cascade); // Cascade delete notifications when the user is deleted
	}
}