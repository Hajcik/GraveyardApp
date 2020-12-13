using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class FixLodging_and_DeadPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadPersonId",
                table: "Lodgings");

            migrationBuilder.AddColumn<int>(
                name: "PeopleLimit",
                table: "Lodgings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LodgingId",
                table: "DeadPeople",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleLimit",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "LodgingId",
                table: "DeadPeople");

            migrationBuilder.AddColumn<int>(
                name: "DeadPersonId",
                table: "Lodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
