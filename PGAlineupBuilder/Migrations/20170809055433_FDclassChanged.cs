using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PGAlineupBuilder.Migrations
{
    public partial class FDclassChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Playerid",
                table: "FDGOLFER",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Playerid",
                table: "FDGOLFER",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
