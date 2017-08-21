using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PGAlineupBuilder.Data;

namespace PGAlineupBuilder.Migrations
{
    [DbContext(typeof(PGAlineupBuilderDbContext))]
    [Migration("20170820070059_BlogPostClasses")]
    partial class BlogPostClasses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PGAlineupBuilder.Models.BlogPost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Content");

                    b.Property<string>("Meta");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PublishedDate");

                    b.Property<string>("URLslug");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("BP");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.BlogPostTag", b =>
                {
                    b.Property<int>("BlogPostID");

                    b.Property<int>("TagID");

                    b.HasKey("BlogPostID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("URLslug");

                    b.HasKey("ID");

                    b.ToTable("BPCAT");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BlogPostID");

                    b.Property<DateTime>("CommentDate");

                    b.Property<string>("Content");

                    b.Property<string>("Meta");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("URLslug");

                    b.HasKey("ID");

                    b.HasIndex("BlogPostID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.DkTourney", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("DKT");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.FDgolfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Exposure");

                    b.Property<int>("FDtourneyID");

                    b.Property<string>("GameInfo");

                    b.Property<string>("Name");

                    b.Property<string>("Playerid");

                    b.Property<int>("Salary");

                    b.Property<string>("Website");

                    b.Property<int>("YearCreated");

                    b.HasKey("ID");

                    b.HasIndex("FDtourneyID");

                    b.ToTable("FDGOLFER");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.FDtourney", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("FDT");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Golfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DkTourneyID");

                    b.Property<double>("Exposure");

                    b.Property<string>("GameInfo");

                    b.Property<string>("Name");

                    b.Property<int>("Playerid");

                    b.Property<int>("Salary");

                    b.Property<string>("Website");

                    b.Property<int>("YearCreated");

                    b.HasKey("ID");

                    b.HasIndex("DkTourneyID");

                    b.ToTable("GOLFER");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("URLslug");

                    b.HasKey("ID");

                    b.ToTable("BPTag");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.BlogPost", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.Category", "Category")
                        .WithMany("BPosts")
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.BlogPostTag", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.BlogPost", "BlogPost")
                        .WithMany("BlogPostTags")
                        .HasForeignKey("BlogPostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PGAlineupBuilder.Models.Tag", "Tag")
                        .WithMany("BlogPostTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Comment", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostID");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.FDgolfer", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.FDtourney")
                        .WithMany("Participants")
                        .HasForeignKey("FDtourneyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Golfer", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.DkTourney")
                        .WithMany("Participants")
                        .HasForeignKey("DkTourneyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
