using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class MatchTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Team_RefTeam",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.AddColumn<bool>(
                name: "isTeamCaptain",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "RefTeam");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    RefMatch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefHomeTeam = table.Column<int>(type: "int", nullable: true),
                    RefGuestTeam = table.Column<int>(type: "int", nullable: true),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefMatchWinner = table.Column<int>(type: "int", nullable: true),
                    Formate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestOf = table.Column<int>(type: "int", nullable: false),
                    TeamRefTeam = table.Column<int>(type: "int", nullable: true),
                    UserRefUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.RefMatch);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_RefGuestTeam",
                        column: x => x.RefGuestTeam,
                        principalTable: "Teams",
                        principalColumn: "RefTeam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_RefHomeTeam",
                        column: x => x.RefHomeTeam,
                        principalTable: "Teams",
                        principalColumn: "RefTeam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_RefMatchWinner",
                        column: x => x.RefMatchWinner,
                        principalTable: "Teams",
                        principalColumn: "RefTeam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamRefTeam",
                        column: x => x.TeamRefTeam,
                        principalTable: "Teams",
                        principalColumn: "RefTeam");
                    table.ForeignKey(
                        name: "FK_Matches_Users_UserRefUser",
                        column: x => x.UserRefUser,
                        principalTable: "Users",
                        principalColumn: "RefUser");
                });

            migrationBuilder.CreateTable(
                name: "MatchResult",
                columns: table => new
                {
                    RefMatchResult = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefMatch = table.Column<int>(type: "int", nullable: true),
                    HomeResult = table.Column<int>(type: "int", nullable: true),
                    GuestResult = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResult", x => x.RefMatchResult);
                    table.ForeignKey(
                        name: "FK_MatchResult_Matches_RefMatch",
                        column: x => x.RefMatch,
                        principalTable: "Matches",
                        principalColumn: "RefMatch");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RefGuestTeam",
                table: "Matches",
                column: "RefGuestTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RefHomeTeam",
                table: "Matches",
                column: "RefHomeTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RefMatchWinner",
                table: "Matches",
                column: "RefMatchWinner");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamRefTeam",
                table: "Matches",
                column: "TeamRefTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserRefUser",
                table: "Matches",
                column: "UserRefUser");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResult_RefMatch",
                table: "MatchResult",
                column: "RefMatch");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_RefTeam",
                table: "Users",
                column: "RefTeam",
                principalTable: "Teams",
                principalColumn: "RefTeam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_RefTeam",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MatchResult");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "isTeamCaptain",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "RefTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Team_RefTeam",
                table: "Users",
                column: "RefTeam",
                principalTable: "Team",
                principalColumn: "RefTeam");
        }
    }
}
