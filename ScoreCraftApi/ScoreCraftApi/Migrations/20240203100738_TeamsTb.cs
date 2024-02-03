using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class TeamsTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    RefTeam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.RefTeam);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefTeam",
                table: "Users",
                column: "RefTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Team_RefTeam",
                table: "Users",
                column: "RefTeam",
                principalTable: "Team",
                principalColumn: "RefTeam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Team_RefTeam",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Users_RefTeam",
                table: "Users");
        }
    }
}
