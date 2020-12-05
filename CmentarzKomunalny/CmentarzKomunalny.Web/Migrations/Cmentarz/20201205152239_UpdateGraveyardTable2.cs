using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class UpdateGraveyardTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdGraveyard",
                table: "GraveyardLimits",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GraveyardLimits",
                table: "GraveyardLimits",
                column: "IdGraveyard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GraveyardLimits",
                table: "GraveyardLimits");

            migrationBuilder.DropColumn(
                name: "IdGraveyard",
                table: "GraveyardLimits");
        }
    }
}
