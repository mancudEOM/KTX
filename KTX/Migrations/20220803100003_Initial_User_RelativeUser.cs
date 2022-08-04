using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Initial_User_RelativeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "HistoryRent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    DueDateRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HistoryRentId = table.Column<int>(type: "int", nullable: true),
                    Semeter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateRent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_HistoryRent_HistoryRentId",
                        column: x => x.HistoryRentId,
                        principalTable: "HistoryRent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRent_UserId",
                table: "HistoryRent",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rent_HistoryRentId",
                table: "Rent",
                column: "HistoryRentId");
        }
    }
}
