using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_SupervisorId",
                table: "Volunteers",
                column: "SupervisorId");

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
                name: "VolunteerTrainer");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_SupervisorId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "AvailableHours",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "TrainingStartDate",
                table: "Volunteers");
        }
    }
}
