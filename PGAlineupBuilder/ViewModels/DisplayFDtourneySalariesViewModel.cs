using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using PGAlineupBuilder.Models;

namespace PGAlineupBuilder.ViewModels
{
    public class DisplayFDtourneySalariesViewModel
    {
        public FDtourney FDname { get; set; }

        public IList<FDgolfer> TourneyParticipants { get; set; }

        [Required(ErrorMessage = "Minimum 1 lineup Maximum 150")]
        [Range(1, 150)]
        [Display(Name = "How many 8-man Lineups do you want to build?")]
        public int NumberOfRosters { get; set; }

        [Required(ErrorMessage = "The max salary is $60,000 on FanDuel")]
        [Range(50500, 60000)]
        [Display(Name = "What's the max salary you want used for your rosters?")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "Please use at least $48,500 of the available Salary")]
        [Range(48500, 60000)]
        [Display(Name = "What's the salary floor for your rosters?")]
        public int MinSalary { get; set; }

        public DisplayFDtourneySalariesViewModel()
        {
            // List<Golfer> DKParticipants = new List<Golfer>();
            TourneyParticipants = new List<FDgolfer>();
            FDname = new FDtourney();

            NumberOfRosters = 1;
            MaxSalary = 60000;
            MinSalary = 48500;
        }

        public DisplayFDtourneySalariesViewModel(FDtourney fdt, List<FDgolfer> fdtGolfers)
        {
            TourneyParticipants = new List<FDgolfer>();

            foreach (var golfer in fdtGolfers)
            {
                TourneyParticipants.Add(golfer);

            }

            NumberOfRosters = 1;
            MaxSalary = 60000;
            MinSalary = 48500;
            FDname = fdt;

        }
    }
}
