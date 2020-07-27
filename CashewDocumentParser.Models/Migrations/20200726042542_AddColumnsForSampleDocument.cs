using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddColumnsForSampleDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "TB_SampleDocument");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "TB_SampleDocument",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilenameWithoutExtension",
                table: "TB_SampleDocument",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "TB_SampleDocument");

            migrationBuilder.DropColumn(
                name: "FilenameWithoutExtension",
                table: "TB_SampleDocument");

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "TB_SampleDocument",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
