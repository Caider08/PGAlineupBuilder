using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using System.IO;

namespace PGAlineupBuilder.ViewModels
{
    public class DisplayTourneySalariesViewModel
    {
        public DkTourney Name { get; set; }

        public IList<Golfer> Participants { get; set; }

        public int NumberOfRosters { get; set; }

        public int MaxSalary { get; set; }

        public int MinSalary { get; set; }

        public DisplayTourneySalariesViewModel()
        {
           // List<Golfer> DKParticipants = new List<Golfer>();
        }

        public DisplayTourneySalariesViewModel(DkTourney dkt, IEnumerable<Golfer> dktGolfers)
        {
            Participants = new List<Golfer>();
            
            foreach (var golfer in dktGolfers)
            {
                Participants.Add(golfer);
            }

            dkt = Name;
           
        }


            



    }
}
