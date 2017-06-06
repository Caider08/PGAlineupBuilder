using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PGAlineupBuilder.Data;

namespace PGAlineupBuilder.Migrations
{
    [DbContext(typeof(PGAlineupBuilderDbContext))]
    [Migration("20170606054728_DKcsv")]
    partial class DKcsv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PGAlineupBuilder.Models.DKcsvUpload", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte[]>("csvUpload");

                    b.HasKey("ID");

                    b.ToTable("DKup");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.DKsalarys", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("DKS");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Golfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DKsalarysID");

                    b.Property<string>("Name");

                    b.Property<int>("Salary");

                    b.HasKey("ID");

                    b.HasIndex("DKsalarysID");

                    b.ToTable("Golfer");
                });

            modelBuilder.Entity("PGAlineupBuilder.Models.Golfer", b =>
                {
                    b.HasOne("PGAlineupBuilder.Models.DKsalarys")
                        .WithMany("Golfers")
                        .HasForeignKey("DKsalarysID");
                });
        }
    }
}
