using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Init_Rent_HistoryRent5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HistoryRents_UserId",
                table: "HistoryRents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRents_Users_UserId",
                table: "HistoryRents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRents_Users_UserId",
                table: "HistoryRents");

            migrationBuilder.DropIndex(
                name: "IX_HistoryRents_UserId",
                table: "HistoryRents");
        }
    }
}
