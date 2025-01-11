using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45422d1a-dcce-4b72-a93b-dcf6356e4106", null, "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45422d1a-dcce-4b72-a93b-dcf6356e4106");
        }
    }
}
