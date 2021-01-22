using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashewDocumentParser.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_RoleClaim_TB_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ExtractQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ExtractQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ExtractQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ImportQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ImportQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ImportQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_IntegrationQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IntegrationQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_IntegrationQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PreprocessingQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PreprocessingQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PreprocessingQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ProcessedQueue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    FilenameWithoutExtension = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Fullpath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ProcessedQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ProcessedQueue_TB_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TB_Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_UserClaim_TB_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TB_UserLogin_TB_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TB_UserRole_TB_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_UserRole_TB_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TB_UserToken_TB_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ExtractQueue_TemplateId",
                table: "TB_ExtractQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ImportQueue_TemplateId",
                table: "TB_ImportQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_IntegrationQueue_TemplateId",
                table: "TB_IntegrationQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PreprocessingQueue_TemplateId",
                table: "TB_PreprocessingQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ProcessedQueue_TemplateId",
                table: "TB_ProcessedQueue",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RoleClaim_RoleId",
                table: "TB_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TB_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_UserClaim_UserId",
                table: "TB_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_UserLogin_UserId",
                table: "TB_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_UserRole_RoleId",
                table: "TB_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TB_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TB_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ExtractQueue");

            migrationBuilder.DropTable(
                name: "TB_ImportQueue");

            migrationBuilder.DropTable(
                name: "TB_IntegrationQueue");

            migrationBuilder.DropTable(
                name: "TB_PreprocessingQueue");

            migrationBuilder.DropTable(
                name: "TB_ProcessedQueue");

            migrationBuilder.DropTable(
                name: "TB_RoleClaim");

            migrationBuilder.DropTable(
                name: "TB_UserClaim");

            migrationBuilder.DropTable(
                name: "TB_UserLogin");

            migrationBuilder.DropTable(
                name: "TB_UserRole");

            migrationBuilder.DropTable(
                name: "TB_UserToken");

            migrationBuilder.DropTable(
                name: "TB_Template");

            migrationBuilder.DropTable(
                name: "TB_Roles");

            migrationBuilder.DropTable(
                name: "TB_Users");
        }
    }
}
