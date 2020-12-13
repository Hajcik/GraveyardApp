using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class Fix2Lodging_and_DeadPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Grave_graveIdGrave",
                table: "Lodgings");

            migrationBuilder.DropTable(
                name: "Grave");

            migrationBuilder.DropIndex(
                name: "IX_Lodgings_graveIdGrave",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "graveIdGrave",
                table: "Lodgings");

            migrationBuilder.AlterColumn<string>(
                name: "DateOfDeath_Obituary",
                table: "Obituaries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DateOfPublication",
                table: "News",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "isReserved",
                table: "Lodgings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReserved",
                table: "Lodgings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfDeath_Obituary",
                table: "Obituaries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfPublication",
                table: "News",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "graveIdGrave",
                table: "Lodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Grave",
                columns: table => new
                {
                    IdGrave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    graveType = table.Column<int>(type: "int", nullable: false),
                    isReserved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grave", x => x.IdGrave);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_graveIdGrave",
                table: "Lodgings",
                column: "graveIdGrave");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Grave_graveIdGrave",
                table: "Lodgings",
                column: "graveIdGrave",
                principalTable: "Grave",
                principalColumn: "IdGrave",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
