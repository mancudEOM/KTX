using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTX.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRents_Users_UserId",
                table: "HistoryRents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_HistoryRents_HistoryRentId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "HistoryBills");

            migrationBuilder.DropTable(
                name: "ListPosts");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoomId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryRents",
                table: "HistoryRents");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Users");

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

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryRents",
                table: "HistoryRents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ListPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePost = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListPostId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_ListPosts_ListPostId",
                        column: x => x.ListPostId,
                        principalTable: "ListPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryBills_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
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
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_HistoryBills_HistoryBillId",
                        column: x => x.HistoryBillId,
                        principalTable: "HistoryBills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomId",
                table: "Users",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_HistoryBillId",
                table: "Bills",
                column: "HistoryBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryBills_RoomId",
                table: "HistoryBills",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ListPostId",
                table: "Posts",
                column: "ListPostId");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
