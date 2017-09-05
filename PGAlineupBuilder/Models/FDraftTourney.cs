using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class FDraftTourney
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public IList<FDraftGolfer> Participants { get; set; }


        public FDraftTourney()
        {
            List<FDraftGolfer> Participants = new List<FDraftGolfer>();
        }

        public FDraftTourney(IEnumerable<FDraftGolfer> golfers)
        {
            Participants = new List<FDraftGolfer>();

            foreach (var golfer in golfers)
            {
                Participants.Add(golfer);
            }
        }
    }
}
