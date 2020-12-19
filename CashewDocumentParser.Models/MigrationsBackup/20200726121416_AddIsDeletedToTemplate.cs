using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddIsDeletedToTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TB_Template",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TB_Template");
        }
    }
}
