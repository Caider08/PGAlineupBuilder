using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PGAlineupBuilder.Migrations
{
    public partial class BlogPostClassesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BP_BlogPostID",
                table: "BlogPostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTag_BPTag_TagID",
                table: "BlogPostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag");

            migrationBuilder.RenameTable(
                name: "BlogPostTag",
                newName: "BPostTag");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostTag_TagID",
                table: "BPostTag",
                newName: "IX_BPostTag_TagID");

            migrationBuilder.AddColumn<int>(
                name: "TagID",
                table: "BP",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BPostTag",
                table: "BPostTag",
                columns: new[] { "BlogPostID", "TagID" });

            migrationBuilder.CreateIndex(
                name: "IX_BP_TagID",
                table: "BP",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_BP_BPTag_TagID",
                table: "BP",
                column: "TagID",
                principalTable: "BPTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BPostTag_BP_BlogPostID",
                table: "BPostTag",
                column: "BlogPostID",
                principalTable: "BP",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BPostTag_BPTag_TagID",
                table: "BPostTag",
                column: "TagID",
                principalTable: "BPTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BP_BPTag_TagID",
                table: "BP");

            migrationBuilder.DropForeignKey(
                name: "FK_BPostTag_BP_BlogPostID",
                table: "BPostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BPostTag_BPTag_TagID",
                table: "BPostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BPostTag",
                table: "BPostTag");

            migrationBuilder.DropIndex(
                name: "IX_BP_TagID",
                table: "BP");

            migrationBuilder.DropColumn(
                name: "TagID",
                table: "BP");

            migrationBuilder.RenameTable(
                name: "BPostTag",
                newName: "BlogPostTag");

            migrationBuilder.RenameIndex(
                name: "IX_BPostTag_TagID",
                table: "BlogPostTag",
                newName: "IX_BlogPostTag_TagID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag",
                columns: new[] { "BlogPostID", "TagID" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BP_BlogPostID",
                table: "BlogPostTag",
                column: "BlogPostID",
                principalTable: "BP",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTag_BPTag_TagID",
                table: "BlogPostTag",
                column: "TagID",
                principalTable: "BPTag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
