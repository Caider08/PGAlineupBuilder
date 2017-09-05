using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGAlineupBuilder.Migrations
{
    public partial class FDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FDraftT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDraftT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FDraftG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Exposure = table.Column<double>(nullable: false),
                    FDraftTourneyID = table.Column<int>(nullable: false),
                    GameInfo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Playerid = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false),
                    Website = table.Column<string>(nullable: true),
                    YearCreated = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FDraftG", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FDraftG_FDraftT_FDraftTourneyID",
                        column: x => x.FDraftTourneyID,
                        principalTable: "FDraftT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FDraftG_FDraftTourneyID",
                table: "FDraftG",
                column: "FDraftTourneyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FDraftG");

            migrationBuilder.DropTable(
                name: "FDraftT");
        }
    }
}
