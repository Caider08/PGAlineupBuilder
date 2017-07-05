using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PGAlineupBuilder.Data;

namespace PGAlineupBuilder.Migrations
{
    [DbContext(typeof(PGAlineupBuilderDbContext))]
    [Migration("20170705063117_BigClassChanges")]
    partial class BigClassChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PGAlineupBuilder.Models.DkTourney", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("DKT");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Golfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DkTourneyID");

                    b.Property<int>("Exposure");

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
