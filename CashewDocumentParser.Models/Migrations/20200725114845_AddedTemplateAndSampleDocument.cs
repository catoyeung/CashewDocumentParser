using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddedTemplateAndSampleDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_SampleDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SampleDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Template", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_SampleDocument");

            migrationBuilder.DropTable(
                name: "TB_Template");
        }
    }
}
