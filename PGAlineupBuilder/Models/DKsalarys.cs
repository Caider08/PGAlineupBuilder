using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PGAlineupBuilder.Models;


namespace PGAlineupBuilder.Models
{
    public class DKsalarys
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] csvUpload { get; set; }

       // public IList<Golfer> Golfers { get; set; }


    }
}
