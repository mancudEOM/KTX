using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Initial_Rent_HistoryRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryRent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryRent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryRent_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semeter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDateRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HistoryRentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_HistoryRent_HistoryRentId",
                        column: x => x.HistoryRentId,
                        principalTable: "HistoryRent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRent_UserId",
                table: "HistoryRent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_HistoryRentId",
                table: "Rent",
                column: "HistoryRentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "HistoryRent");
        }
    }
}
