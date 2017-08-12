using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class FDlineup
    {

        public List<string> Lineup { get; set; }

        public IList<FDgolfer> LineupGolfers { get; set; }

        public int LineupSalary { get; set; }

        public int LineupID { get; set; }

        public int amount_golfers { get; set; }

        //private static int lineup_Counter = 0;

        public FDlineup()
        {
            amount_golfers = 0;
            LineupSalary = 0;
            LineupID = 0;
            LineupGolfers = new List<FDgolfer>();
            Lineup = new List<string>();

            //this.LineupID = System.Threading.Interlocked.Increment(ref lineup_Counter);
        }


    }
}

