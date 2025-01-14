using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAnimalAndAnimalProfileEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Animals",
                newName: "Passport");

            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "AnimalProfiles",
                newName: "Contract");

            migrationBuilder.AddColumn<string>(
                name: "AvailableHours",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SupervisorId",
                table: "Volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingStartDate",
                table: "Volunteers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "YearOfBirth",
                table: "Animals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "Sterilized",
                table: "Animals",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalType",
                table: "Animals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string[]>(
                name: "ImageUrls",
                table: "Animals",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<int>(
                name: "ContractState",
                table: "AnimalProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShelterCheckIn",
                table: "AnimalProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShiftType = table.Column<int>(type: "integer", nullable: false),
                    VolunteerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerTrainer",
                columns: table => new
                {
                    TrainerId = table.Column<string>(type: "text", nullable: false),
                    VolunteerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerTrainer", x => new { x.TrainerId, x.VolunteerId });
                    table.ForeignKey(
                        name: "FK_VolunteerTrainer_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VolunteerTrainer_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45422d1a-dcce-4b72-a93b-dcf6356e4106", null, "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_SupervisorId",
                table: "Volunteers",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_VolunteerId",
                table: "Shifts",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerTrainer_VolunteerId",
                table: "VolunteerTrainer",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_AspNetUsers_SupervisorId",
                table: "Volunteers",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_AspNetUsers_SupervisorId",
                table: "Volunteers");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "VolunteerTrainer");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_SupervisorId",
                table: "Volunteers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45422d1a-dcce-4b72-a93b-dcf6356e4106");

            migrationBuilder.DropColumn(
                name: "AvailableHours",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "TrainingStartDate",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ContractState",
                table: "AnimalProfiles");

            migrationBuilder.DropColumn(
                name: "ShelterCheckIn",
                table: "AnimalProfiles");

            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "Animals",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Contract",
                table: "AnimalProfiles",
                newName: "Passport");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfBirth",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Sterilized",
                table: "Animals",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalType",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
