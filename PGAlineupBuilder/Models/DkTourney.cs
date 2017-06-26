using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class DkTourney
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<IEnumerable> Golfer { get; set; }
    }
}
