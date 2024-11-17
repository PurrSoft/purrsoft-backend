using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVolunteerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedArea",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Volunteers");

            migrationBuilder.AddColumn<string[]>(
                name: "Tasks",
                table: "Volunteers",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Volunteers");

            migrationBuilder.AddColumn<string>(
                name: "AssignedArea",
                table: "Volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Volunteers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Volunteers",
                type: "text",
                nullable: true);
        }
    }
}
