using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGAlineupBuilder.Migrations
{
    public partial class FanDuelClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FDT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FDGOLFER",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Exposure = table.Column<double>(nullable: false),
                    FDtourneyID = table.Column<int>(nullable: false),
                    GameInfo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Playerid = table.Column<int>(nullable: false),
                    Salary = table.Column<int>(nullable: false),
                    Website = table.Column<string>(nullable: true),
                    YearCreated = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDGOLFER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FDGOLFER_FDT_FDtourneyID",
                        column: x => x.FDtourneyID,
                        principalTable: "FDT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FDGOLFER_FDtourneyID",
                table: "FDGOLFER",
                column: "FDtourneyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FDGOLFER");

            migrationBuilder.DropTable(
                name: "FDT");
        }
    }
}
