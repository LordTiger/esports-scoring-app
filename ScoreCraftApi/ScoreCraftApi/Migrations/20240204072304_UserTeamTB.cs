using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class UserTeamTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchResult_Matches_RefMatch",
                table: "MatchResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchResult",
                table: "MatchResult");

            migrationBuilder.RenameTable(
                name: "MatchResult",
                newName: "MatchResults");

            migrationBuilder.RenameColumn(
                name: "isTeamCaptain",
                table: "Users",
                newName: "IsTeamCaptain");

            migrationBuilder.RenameIndex(
                name: "IX_MatchResult_RefMatch",
                table: "MatchResults",
                newName: "IX_MatchResults_RefMatch");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchResults",
                table: "MatchResults",
                column: "RefMatchResult");

            migrationBuilder.CreateTable(
                name: "UserTeam",
                columns: table => new
                {
                    RefUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeam", x => new { x.RefUser, x.RefTeam });
                    table.ForeignKey(
                        name: "FK_UserTeam_Teams_RefTeam",
                        column: x => x.RefTeam,
                        principalTable: "Teams",
                        principalColumn: "RefTeam",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeam_Users_RefUser",
                        column: x => x.RefUser,
                        principalTable: "Users",
                        principalColumn: "RefUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_RefTeam",
                table: "UserTeam",
                column: "RefTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResults_Matches_RefMatch",
                table: "MatchResults",
                column: "RefMatch",
                principalTable: "Matches",
                principalColumn: "RefMatch");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchResults_Matches_RefMatch",
                table: "MatchResults");

            migrationBuilder.DropTable(
                name: "UserTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchResults",
                table: "MatchResults");

            migrationBuilder.RenameTable(
                name: "MatchResults",
                newName: "MatchResult");

            migrationBuilder.RenameColumn(
                name: "IsTeamCaptain",
                table: "Users",
                newName: "isTeamCaptain");

            migrationBuilder.RenameIndex(
                name: "IX_MatchResults_RefMatch",
                table: "MatchResult",
                newName: "IX_MatchResult_RefMatch");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchResult",
                table: "MatchResult",
                column: "RefMatchResult");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResult_Matches_RefMatch",
                table: "MatchResult",
                column: "RefMatch",
                principalTable: "Matches",
                principalColumn: "RefMatch");
        }
    }
}
