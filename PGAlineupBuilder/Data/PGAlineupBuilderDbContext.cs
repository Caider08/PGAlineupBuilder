using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PGAlineupBuilder.Models;

namespace PGAlineupBuilder.Data
{
    public class PGAlineupBuilderDbContext: DbContext
    {

        public DbSet<DkTourney> DKT { get; set; }

        public DbSet<Golfer> GOLFER { get; set; }

        public DbSet<FDtourney> FDT { get; set; }

        public DbSet<FDgolfer> FDGOLFER { get; set; }

        public DbSet<BlogPost> BP { get; set; }

        public DbSet<Category> BPCAT { get; set; }

        public DbSet <Tag> BPTag { get; set; }


        public PGAlineupBuilderDbContext(DbContextOptions<PGAlineupBuilderDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

       
    }
}
