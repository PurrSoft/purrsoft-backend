using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AnimalProfileFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles");

            migrationBuilder.DropIndex(
                name: "IX_AnimalProfiles_AnimalId",
                table: "AnimalProfiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AnimalProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AnimalProfiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles",
                column: "Id");
            
            migrationBuilder.CreateIndex(
                name: "IX_AnimalProfiles_AnimalId",
                table: "AnimalProfiles",
                column: "AnimalId",
                unique: true);
            
        }
    }
}
