using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddedQueues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ClassificationQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false),
                    ProcessStage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ClassificationQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ClassificationQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_OCRQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false),
                    ProcessStage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OCRQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_OCRQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ScriptingQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false),
                    ProcessStage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ScriptingQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ScriptingQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ClassificationQueue_TemplateId",
                table: "TB_ClassificationQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_OCRQueue_TemplateId",
                table: "TB_OCRQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ScriptingQueue_TemplateId",
                table: "TB_ScriptingQueue",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ClassificationQueue");

            migrationBuilder.DropTable(
                name: "TB_OCRQueue");

            migrationBuilder.DropTable(
                name: "TB_ScriptingQueue");
        }
    }
}
