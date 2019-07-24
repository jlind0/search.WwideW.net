using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortId = table.Column<Guid>(nullable: true),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortenedUrl",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Raw = table.Column<string>(nullable: true),
                    Short = table.Column<string>(nullable: true),
                    UrlSetId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortenedUrl_UrlSet_UrlSetId",
                        column: x => x.UrlSetId,
                        principalTable: "UrlSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrl_Short",
                table: "ShortenedUrl",
                column: "Short",
                unique: true,
                filter: "[Short] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrl_UrlSetId",
                table: "ShortenedUrl",
                column: "UrlSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlSet_Key",
                table: "UrlSet",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UrlSet_ShortId",
                table: "UrlSet",
                column: "ShortId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlSet_ShortenedUrl_ShortId",
                table: "UrlSet",
                column: "ShortId",
                principalTable: "ShortenedUrl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenedUrl_UrlSet_UrlSetId",
                table: "ShortenedUrl");

            migrationBuilder.DropTable(
                name: "UrlSet");

            migrationBuilder.DropTable(
                name: "ShortenedUrl");
        }
    }
}
