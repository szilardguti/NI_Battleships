using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleships.Migrations
{
    public partial class AddJSONPropertiesToGameAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstPlayerBoardStatus",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstPlayerShips",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPlayerBoardStatus",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPlayerShips",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlayerBoardStatus",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "FirstPlayerShips",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "SecondPlayerBoardStatus",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "SecondPlayerShips",
                table: "Actions");
        }
    }
}
