using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvinashBackEndAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginID",
                table: "Users",
                newName: "LoginName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginName",
                table: "Users",
                newName: "LoginID");
        }
    }
}
