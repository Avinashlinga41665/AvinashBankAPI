using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvinashBackEndAPI.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class LocalDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Accounts");
        }
    }
}
