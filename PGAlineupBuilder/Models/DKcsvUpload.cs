using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace PGAlineupBuilder.Models
{
    public class DKcsvUpload
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] csvUpload { get; set; }
    }
}
