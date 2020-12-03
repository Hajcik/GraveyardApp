using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeadPeople",
                columns: table => new
                {
                    IdDeadPerson = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 75, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfDeath = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadPeople", x => x.IdDeadPerson);
                });

            migrationBuilder.CreateTable(
                name: "Grave",
                columns: table => new
                {
                    IdGrave = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isReserved = table.Column<bool>(nullable: false),
                    graveType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grave", x => x.IdGrave);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    DateOfPublication = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obituaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    DateOfDeath_Obituary = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obituaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    IdLodge = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeadPersonId = table.Column<int>(nullable: false),
                    LodgingType = table.Column<int>(nullable: false),
                    graveIdGrave = table.Column<int>(nullable: false),
                    isMonument = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.IdLodge);
                    table.ForeignKey(
                        name: "FK_Lodgings_Grave_graveIdGrave",
                        column: x => x.graveIdGrave,
                        principalTable: "Grave",
                        principalColumn: "IdGrave",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_graveIdGrave",
                table: "Lodgings",
                column: "graveIdGrave");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeadPeople");

            migrationBuilder.DropTable(
                name: "Lodgings");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Obituaries");

            migrationBuilder.DropTable(
                name: "Grave");
        }
    }
}
