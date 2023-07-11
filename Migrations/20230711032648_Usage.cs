using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    /// <inheritdoc />
    public partial class Usage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usages",
                table: "TinyUrls",
                newName: "UsagesCount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateExpired",
                table: "TinyUrls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Usages",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tinyUrlId = table.Column<int>(type: "int", nullable: true),
                    UseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Region = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usages", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_Usages_TinyUrls_tinyUrlId",
                        column: x => x.tinyUrlId,
                        principalTable: "TinyUrls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usages_tinyUrlId",
                table: "Usages",
                column: "tinyUrlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usages");

            migrationBuilder.RenameColumn(
                name: "UsagesCount",
                table: "TinyUrls",
                newName: "Usages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateExpired",
                table: "TinyUrls",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
