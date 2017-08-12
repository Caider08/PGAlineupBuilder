using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class FDtourney
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<FDgolfer> Participants { get; set; }


        public FDtourney()
        {
            List<FDgolfer> Participants = new List<FDgolfer>();
        }

        public FDtourney(IEnumerable<FDgolfer> golfers)
        {
            Participants = new List<FDgolfer>();

            foreach (var golfer in golfers)
            {
                Participants.Add(golfer);
            }
        }
    }
}
