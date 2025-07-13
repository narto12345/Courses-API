using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Migrations
{
    /// <inheritdoc />
    public partial class UserUserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentityId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserIdentityId",
                table: "Users",
                column: "UserIdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_UserIdentityId",
                table: "Users",
                column: "UserIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_UserIdentityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserIdentityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserIdentityId",
                table: "Users");
        }
    }
}
