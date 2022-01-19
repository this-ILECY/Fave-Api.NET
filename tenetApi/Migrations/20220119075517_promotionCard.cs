using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tenetApi.Migrations
{
    public partial class promotionCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "promotion");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "promotion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "promotion");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "promotion",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
