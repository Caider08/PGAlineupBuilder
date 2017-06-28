using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGAlineupBuilder.Migrations
{
    public partial class DroppedDKS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Golfer_DKS_DKsalarysID",
                table: "Golfer");

            migrationBuilder.DropTable(
                name: "DKS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Golfer",
                table: "Golfer");

            migrationBuilder.RenameTable(
                name: "Golfer",
                newName: "GOLFER");

            migrationBuilder.RenameColumn(
                name: "DKsalarysID",
                table: "GOLFER",
                newName: "DkTourneyID");

            migrationBuilder.RenameIndex(
                name: "IX_Golfer_DKsalarysID",
                table: "GOLFER",
                newName: "IX_GOLFER_DkTourneyID");

            migrationBuilder.AddColumn<string>(
                name: "GameInfo",
                table: "GOLFER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GOLFER",
                table: "GOLFER",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "DKT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DKT", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER",
                column: "DkTourneyID",
                principalTable: "DKT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GOLFER_DKT_DkTourneyID",
                table: "GOLFER");

            migrationBuilder.DropTable(
                name: "DKT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GOLFER",
                table: "GOLFER");

            migrationBuilder.DropColumn(
                name: "GameInfo",
                table: "GOLFER");

            migrationBuilder.RenameTable(
                name: "GOLFER",
                newName: "Golfer");

            migrationBuilder.RenameColumn(
                name: "DkTourneyID",
                table: "Golfer",
                newName: "DKsalarysID");

            migrationBuilder.RenameIndex(
                name: "IX_GOLFER_DkTourneyID",
                table: "Golfer",
                newName: "IX_Golfer_DKsalarysID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Golfer",
                table: "Golfer",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "DKS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    csvUpload = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DKS", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Golfer_DKS_DKsalarysID",
                table: "Golfer",
                column: "DKsalarysID",
                principalTable: "DKS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
