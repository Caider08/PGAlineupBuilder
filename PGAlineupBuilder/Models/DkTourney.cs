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

        public IList<Golfer> Participants { get; set; }


        public DkTourney()
        {
            List<Golfer> Participants = new List<Golfer>();
        }

        public DkTourney(IEnumerable<Golfer> golfers)
        {
            Participants = new List<Golfer>();

            foreach(var golfer in golfers)
            {
                Participants.Add(golfer);
            }
        }
    }
}
