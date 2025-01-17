using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Requests",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Requests");
        }
    }
}
