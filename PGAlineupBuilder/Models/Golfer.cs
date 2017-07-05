using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class Golfer
    {
        public int ID { get; set; }

        public int DkTourneyID { get; set; }
        
        public int Playerid { get; set; }

        public int Salary { get; set; }

        public string Name { get; set; }

        public string GameInfo { get; set; }

        public int YearCreated { get; set; }

        public int Exposure { get; set; }

        public string Website { get; set; }


        public Golfer()
        {
            Exposure = 0;
        }
    }
}
