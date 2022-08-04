using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Update_Rent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
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
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_HistoryRents_HistoryRentId",
                        column: x => x.HistoryRentId,
                        principalTable: "HistoryRents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_HistoryRentId",
                table: "Rents",
                column: "HistoryRentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
