using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyAnimalFoster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalFosters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartFosteringDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndFosteringDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SupervisingComment = table.Column<string>(type: "text", nullable: true),
                    AnimalId = table.Column<Guid>(type: "uuid", nullable: false),
                    FosterId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalFosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalFosters_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalFosters_Fosters_FosterId",
                        column: x => x.FosterId,
                        principalTable: "Fosters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalFosters_AnimalId",
                table: "AnimalFosters",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalFosters_FosterId",
                table: "AnimalFosters",
                column: "FosterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalFosters");
        }
    }
}
