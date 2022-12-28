using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleships.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPlayerHitCount = table.Column<int>(type: "int", nullable: false),
                    SecondPlayerHitCount = table.Column<int>(type: "int", nullable: false),
                    RoundCount = table.Column<int>(type: "int", nullable: false),
                    IsInProgress = table.Column<bool>(type: "bit", nullable: false),
                    WinnerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Results_GameResultId",
                        column: x => x.GameResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_GameResultId",
                table: "Actions",
                column: "GameResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
