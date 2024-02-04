using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRefTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_RefTeam",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeam_Teams_RefTeam",
                table: "UserTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeam_Users_RefUser",
                table: "UserTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam");

            migrationBuilder.RenameTable(
                name: "UserTeam",
                newName: "UserTeams");

            migrationBuilder.RenameColumn(
                name: "RefTeam",
                table: "Users",
                newName: "TeamRefTeam");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RefTeam",
                table: "Users",
                newName: "IX_Users_TeamRefTeam");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeam_RefTeam",
                table: "UserTeams",
                newName: "IX_UserTeams_RefTeam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams",
                columns: new[] { "RefUser", "RefTeam" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_TeamRefTeam",
                table: "Users",
                column: "TeamRefTeam",
                principalTable: "Teams",
                principalColumn: "RefTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Teams_RefTeam",
                table: "UserTeams",
                column: "RefTeam",
                principalTable: "Teams",
                principalColumn: "RefTeam",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Users_RefUser",
                table: "UserTeams",
                column: "RefUser",
                principalTable: "Users",
                principalColumn: "RefUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_TeamRefTeam",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Teams_RefTeam",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Users_RefUser",
                table: "UserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams");

            migrationBuilder.RenameTable(
                name: "UserTeams",
                newName: "UserTeam");

            migrationBuilder.RenameColumn(
                name: "TeamRefTeam",
                table: "Users",
                newName: "RefTeam");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TeamRefTeam",
                table: "Users",
                newName: "IX_Users_RefTeam");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_RefTeam",
                table: "UserTeam",
                newName: "IX_UserTeam_RefTeam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam",
                columns: new[] { "RefUser", "RefTeam" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_RefTeam",
                table: "Users",
                column: "RefTeam",
                principalTable: "Teams",
                principalColumn: "RefTeam");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeam_Teams_RefTeam",
                table: "UserTeam",
                column: "RefTeam",
                principalTable: "Teams",
                principalColumn: "RefTeam",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeam_Users_RefUser",
                table: "UserTeam",
                column: "RefUser",
                principalTable: "Users",
                principalColumn: "RefUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
