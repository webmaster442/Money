using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserAddedToSpendings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Spendings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_UserId",
                table: "Spendings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spendings_AspNetUsers_UserId",
                table: "Spendings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spendings_AspNetUsers_UserId",
                table: "Spendings");

            migrationBuilder.DropIndex(
                name: "IX_Spendings_UserId",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Spendings");
        }
    }
}
