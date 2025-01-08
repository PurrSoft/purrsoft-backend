﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PurrSoft.Persistence;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    [DbContext(typeof(PurrSoftDbContext))]
    partial class PurrSoftDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AnimalType")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Sterilized")
                        .HasColumnType("boolean");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.AnimalFosterMap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndFosteringDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FosterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartFosteringDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SupervisingComment")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("FosterId");

                    b.ToTable("AnimalFosters");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.AnimalProfile", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("text");

                    b.Property<string>("AdditionalMedicalInfo")
                        .HasColumnType("text");

                    b.Property<string>("CoronavirusVaccine")
                        .HasColumnType("text");

                    b.Property<string>("CurrentDisease")
                        .HasColumnType("text");

                    b.Property<string>("CurrentMedication")
                        .HasColumnType("text");

                    b.Property<string>("CurrentTreatment")
                        .HasColumnType("text");

                    b.Property<string>("EarMiteTreatment")
                        .HasColumnType("text");

                    b.Property<string>("ExternalDeworming")
                        .HasColumnType("text");

                    b.Property<string>("FIVFeLVTest")
                        .HasColumnType("text");

                    b.Property<string>("GiardiaTest")
                        .HasColumnType("text");

                    b.Property<string>("IntakeNotes")
                        .HasColumnType("text");

                    b.Property<string>("InternalDeworming")
                        .HasColumnType("text");

                    b.Property<string>("MedicalAppointments")
                        .HasColumnType("text");

                    b.Property<string>("Microchip")
                        .HasColumnType("text");

                    b.Property<string>("MultivalentVaccine")
                        .HasColumnType("text");

                    b.Property<string>("Passport")
                        .HasColumnType("text");

                    b.Property<string>("PastDisease")
                        .HasColumnType("text");

                    b.Property<string>("RabiesVaccine")
                        .HasColumnType("text");

                    b.Property<string>("RefillReminders")
                        .HasColumnType("text");

                    b.PrimitiveCollection<List<string>>("UsefulLinks")
                        .HasColumnType("jsonb");

                    b.HasKey("AnimalId");

                    b.ToTable("AnimalProfiles");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.ApplicationLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CallStack")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExceptionMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExceptionSource")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Logger")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationLogs");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Foster", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("AnimalFosteredCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExperienceLevel")
                        .HasColumnType("text");

                    b.Property<bool>("HasOtherAnimals")
                        .HasColumnType("boolean");

                    b.Property<string>("HomeDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MaxAnimalsAllowed")
                        .HasColumnType("integer");

                    b.Property<string>("OtherAnimalDetails")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.ToTable("Fosters");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c68c49f4-7cff-4c4e-a836-f670f386c0f0",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "5f50c0ac-fadf-4792-af6a-54dcd5c3aab3",
                            Name = "Volunteer",
                            NormalizedName = "VOLUNTEER"
                        },
                        new
                        {
                            Id = "16f9f85a-1389-489a-87d4-cef974323047",
                            Name = "Foster",
                            NormalizedName = "FOSTER"
                        },
                        new
                        {
                            Id = "45422d1a-dcce-4b72-a93b-dcf6356e4106",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Shift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ShiftStatus")
                        .HasColumnType("integer");

                    b.Property<int>("ShiftType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VolunteerId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Volunteer", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("AvailableHours")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastShiftDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("SupervisorId")
                        .HasColumnType("text");

                    b.PrimitiveCollection<string[]>("Tasks")
                        .HasColumnType("text[]");

                    b.Property<int>("Tier")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TrainingStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("VolunteerTrainer", b =>
                {
                    b.Property<string>("TrainerId")
                        .HasColumnType("text");

                    b.Property<string>("VolunteerId")
                        .HasColumnType("text");

                    b.HasKey("TrainerId", "VolunteerId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("VolunteerTrainer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.AnimalFosterMap", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.Animal", "Animal")
                        .WithMany("FosteredBy")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PurrSoft.Domain.Entities.Foster", "Foster")
                        .WithMany("FosteredAnimals")
                        .HasForeignKey("FosterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Foster");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.AnimalProfile", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.Animal", "Animal")
                        .WithOne("AnimalProfile")
                        .HasForeignKey("PurrSoft.Domain.Entities.AnimalProfile", "AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Foster", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("PurrSoft.Domain.Entities.Foster", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Shift", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.Volunteer", "Volunteer")
                        .WithMany("Shifts")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Volunteer", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("PurrSoft.Domain.Entities.Volunteer", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Supervisor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VolunteerTrainer", b =>
                {
                    b.HasOne("PurrSoft.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PurrSoft.Domain.Entities.Volunteer", null)
                        .WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Animal", b =>
                {
                    b.Navigation("AnimalProfile");

                    b.Navigation("FosteredBy");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Foster", b =>
                {
                    b.Navigation("FosteredAnimals");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PurrSoft.Domain.Entities.Volunteer", b =>
                {
                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
