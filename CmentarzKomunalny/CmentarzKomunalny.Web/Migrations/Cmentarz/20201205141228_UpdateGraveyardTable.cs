﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CmentarzKomunalny.Web.Migrations.Cmentarz
{
    public partial class UpdateGraveyardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LimitSectors",
                table: "GraveyardLimits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitSectors",
                table: "GraveyardLimits");
        }
    }
}
