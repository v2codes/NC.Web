using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NC.Core.Migrations
{
    public partial class InitNCWeb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyUserId = table.Column<Guid>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyUserId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: true),
                    IsSystem = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
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
                    Status = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyUserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    BlogId = table.Column<int>(nullable: false),
                    BlogId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Blog_BlogId1",
                        column: x => x.BlogId1,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId1",
                table: "Post",
                column: "BlogId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
