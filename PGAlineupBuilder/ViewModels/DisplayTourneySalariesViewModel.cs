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

        public List<Golfer> DKParticipants { get; set; }

        public int NumberOfRosters { get; set; }

        public int MaxSalary { get; set; }

        public int MinSalary { get; set; }

        public DisplayTourneySalariesViewModel()
        {
        }

        public DisplayTourneySalariesViewModel(DkTourney dkt, List<Golfer> dktGolfers)
        {
            dkt = Name;
            dktGolfers = DKParticipants;
        }


            



    }
}
