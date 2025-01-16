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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
