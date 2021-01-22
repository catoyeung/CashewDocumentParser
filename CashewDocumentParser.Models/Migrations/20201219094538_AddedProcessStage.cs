using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class AddedProcessStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessStage",
                table: "TB_ProcessedQueue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessStage",
                table: "TB_PreprocessingQueue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessStage",
                table: "TB_IntegrationQueue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessStage",
                table: "TB_ImportQueue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessStage",
                table: "TB_ExtractQueue",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessStage",
                table: "TB_ProcessedQueue");

            migrationBuilder.DropColumn(
                name: "ProcessStage",
                table: "TB_PreprocessingQueue");

            migrationBuilder.DropColumn(
                name: "ProcessStage",
                table: "TB_IntegrationQueue");

            migrationBuilder.DropColumn(
                name: "ProcessStage",
                table: "TB_ImportQueue");

            migrationBuilder.DropColumn(
                name: "ProcessStage",
                table: "TB_ExtractQueue");
        }
    }
}
