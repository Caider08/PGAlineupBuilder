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
        public DbSet<DKsalarys> DKS { get; set; }

        public DbSet<DkTourney> DKT { get; set; }


        public PGAlineupBuilderDbContext(DbContextOptions<PGAlineupBuilderDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

       
    }
}
