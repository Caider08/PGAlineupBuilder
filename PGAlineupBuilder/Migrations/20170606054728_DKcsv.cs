using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGAlineupBuilder.Migrations
{
    public partial class DKcsv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DKup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    csvUpload = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DKup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Golfer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DKsalarysID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Golfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Golfer_DKS_DKsalarysID",
                        column: x => x.DKsalarysID,
                        principalTable: "DKS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Golfer_DKsalarysID",
                table: "Golfer",
                column: "DKsalarysID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DKup");

            migrationBuilder.DropTable(
                name: "Golfer");
        }
    }
}
