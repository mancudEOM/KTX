using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class Init_Rent_HistoryRent6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRents_Users_UserId",
                table: "HistoryRents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_HistoryRents_HistoryRentId",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRents",
                table: "HistoryRents");

            migrationBuilder.RenameTable(
                name: "Rents",
                newName: "Rent");

            migrationBuilder.RenameTable(
                name: "HistoryRents",
                newName: "HistoryRent");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_HistoryRentId",
                table: "Rent",
                newName: "IX_Rent_HistoryRentId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRents_UserId",
                table: "HistoryRent",
                newName: "IX_HistoryRent_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rent",
                table: "Rent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRent",
                table: "HistoryRent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRent_Users_UserId",
                table: "HistoryRent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_HistoryRent_HistoryRentId",
                table: "Rent",
                column: "HistoryRentId",
                principalTable: "HistoryRent",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRent_Users_UserId",
                table: "HistoryRent");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_HistoryRent_HistoryRentId",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rent",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRent",
                table: "HistoryRent");

            migrationBuilder.RenameTable(
                name: "Rent",
                newName: "Rents");

            migrationBuilder.RenameTable(
                name: "HistoryRent",
                newName: "HistoryRents");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_HistoryRentId",
                table: "Rents",
                newName: "IX_Rents_HistoryRentId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryRent_UserId",
                table: "HistoryRents",
                newName: "IX_HistoryRents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRents",
                table: "HistoryRents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRents_Users_UserId",
                table: "HistoryRents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_HistoryRents_HistoryRentId",
                table: "Rents",
                column: "HistoryRentId",
                principalTable: "HistoryRents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
