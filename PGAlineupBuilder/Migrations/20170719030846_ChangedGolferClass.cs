using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PGAlineupBuilder.Migrations
{
    public partial class ChangedGolferClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Exposure",
                table: "GOLFER",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Exposure",
                table: "GOLFER",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
