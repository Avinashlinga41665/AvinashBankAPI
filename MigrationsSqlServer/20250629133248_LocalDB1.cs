using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvinashBackEndAPI.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class LocalDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Users");
        }
    }
}
