using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalProfileUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the AnimalProfiles table
            migrationBuilder.CreateTable(
                name: "AnimalProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false), // Primary key
                    CurrentDisease = table.Column<string>(type: "text", nullable: true),
                    CurrentMedication = table.Column<string>(type: "text", nullable: true),
                    PastDisease = table.Column<string>(type: "text", nullable: true),
                    AnimalId = table.Column<Guid>(nullable: false), // Foreign key to Animals table
                    Passport = table.Column<string>(type: "text", nullable: true),
                    Microchip = table.Column<string>(type: "text", nullable: true),
                    ExternalDeworming = table.Column<string>(type: "text", nullable: true),
                    InternalDeworming = table.Column<string>(type: "text", nullable: true),
                    CurrentTreatment = table.Column<string>(type: "text", nullable: true),
                    MultivalentVaccine = table.Column<string>(type: "text", nullable: true),
                    RabiesVaccine = table.Column<string>(type: "text", nullable: true),
                    FIVFeLVTest = table.Column<string>(type: "text", nullable: true),
                    CoronavirusVaccine = table.Column<string>(type: "text", nullable: true),
                    GiardiaTest = table.Column<string>(type: "text", nullable: true),
                    EarMiteTreatment = table.Column<string>(type: "text", nullable: true),
                    IntakeNotes = table.Column<string>(type: "text", nullable: true),
                    AdditionalMedicalInfo = table.Column<string>(type: "text", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    MedicalAppointments = table.Column<string>(type: "text", nullable: true),
                    RefillReminders = table.Column<string>(type: "text", nullable: true),
                    UsefulLinks = table.Column<List<string>>(type: "jsonb", nullable: true) // JSONB column
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalProfiles", x => x.Id); // Primary key
                    table.ForeignKey(
                        name: "FK_AnimalProfiles_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Cascade delete
                });

            // Add index on the foreign key for better performance
            migrationBuilder.CreateIndex(
                name: "IX_AnimalProfiles_AnimalId",
                table: "AnimalProfiles",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the AnimalProfiles table
            migrationBuilder.DropTable(name: "AnimalProfiles");
        }
    }
}
