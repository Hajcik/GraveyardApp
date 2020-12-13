using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class FixContent_ObituaryAndNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "ObituaryContent",
                table: "Obituaries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewsContent",
                table: "News",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObituaryContent",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "NewsContent",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Obituaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
