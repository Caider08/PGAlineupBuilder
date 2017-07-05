using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PGAlineupBuilder.Migrations
{
    public partial class BigClassChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER");

            migrationBuilder.AlterColumn<int>(
                name: "DkTourneyID",
                table: "GOLFER",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Exposure",
                table: "GOLFER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearCreated",
                table: "GOLFER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER",
                column: "DkTourneyID",
                principalTable: "DKT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER");

            migrationBuilder.DropColumn(
                name: "Exposure",
                table: "GOLFER");

            migrationBuilder.DropColumn(
                name: "YearCreated",
                table: "GOLFER");

            migrationBuilder.AlterColumn<int>(
                name: "DkTourneyID",
                table: "GOLFER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER",
                column: "DkTourneyID",
                principalTable: "DKT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
