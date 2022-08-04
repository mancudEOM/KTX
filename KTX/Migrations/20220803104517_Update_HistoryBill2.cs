using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Update_HistoryBill2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElectricityIndexE = table.Column<float>(type: "real", nullable: false),
                    ElectricityIndexS = table.Column<float>(type: "real", nullable: false),
                    HistoryBillId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    WaterIndexE = table.Column<float>(type: "real", nullable: false),
                    WaterIndexS = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_HistoryBills_HistoryBillId",
                        column: x => x.HistoryBillId,
                        principalTable: "HistoryBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_HistoryBillId",
                table: "Bill",
                column: "HistoryBillId");
        }
    }
}
