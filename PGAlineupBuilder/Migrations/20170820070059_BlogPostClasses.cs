using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PGAlineupBuilder.Migrations
{
    public partial class BlogPostClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BPCAT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    URLslug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPCAT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BPTag",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    URLslug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Meta = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: false),
                    URLslug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BP_BPCAT_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "BPCAT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    BlogPostID = table.Column<int>(nullable: false),
                    TagID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.BlogPostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BP_BlogPostID",
                        column: x => x.BlogPostID,
                        principalTable: "BP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BPTag_TagID",
                        column: x => x.TagID,
                        principalTable: "BPTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogPostID = table.Column<int>(nullable: true),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Meta = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    URLslug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_BP_BlogPostID",
                        column: x => x.BlogPostID,
                        principalTable: "BP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BP_CategoryID",
                table: "BP",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagID",
                table: "BlogPostTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BlogPostID",
                table: "Comment",
                column: "BlogPostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "BPTag");

            migrationBuilder.DropTable(
                name: "BP");

            migrationBuilder.DropTable(
                name: "BPCAT");
        }
    }
}
