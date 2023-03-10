using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserAccesses_AccessId",
                table: "UserAccesses",
                column: "AccessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccesses_UserId",
                table: "UserAccesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccesses_AccessId",
                table: "RoleAccesses",
                column: "AccessId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccesses_RoleId",
                table: "RoleAccesses",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccesses_Accesses_AccessId",
                table: "RoleAccesses",
                column: "AccessId",
                principalTable: "Accesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccesses_Roles_RoleId",
                table: "RoleAccesses",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccesses_Accesses_AccessId",
                table: "UserAccesses",
                column: "AccessId",
                principalTable: "Accesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccesses_Users_UserId",
                table: "UserAccesses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccesses_Accesses_AccessId",
                table: "RoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccesses_Roles_RoleId",
                table: "RoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccesses_Accesses_AccessId",
                table: "UserAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccesses_Users_UserId",
                table: "UserAccesses");

            migrationBuilder.DropIndex(
                name: "IX_UserAccesses_AccessId",
                table: "UserAccesses");

            migrationBuilder.DropIndex(
                name: "IX_UserAccesses_UserId",
                table: "UserAccesses");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccesses_AccessId",
                table: "RoleAccesses");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccesses_RoleId",
                table: "RoleAccesses");
        }
    }
}
