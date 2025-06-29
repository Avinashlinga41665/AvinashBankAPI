using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvinashBackEndAPI.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class UpdateSqlServer7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Accounts",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                newName: "IX_Accounts_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Accounts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts",
                newName: "IX_Accounts_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
