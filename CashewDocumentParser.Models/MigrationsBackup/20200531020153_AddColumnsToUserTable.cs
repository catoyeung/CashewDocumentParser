using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddColumnsToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "TB_Users");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TB_Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TB_Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TB_Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TB_Users");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
