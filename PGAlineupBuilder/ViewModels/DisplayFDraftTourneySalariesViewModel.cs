using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PGAlineupBuilder.ViewModels
{
    public class DisplayFDraftTourneySalariesViewModel
    {
        public FDraftTourney FDraftName { get; set; }

        public IList<FDraftGolfer> TourneyParticipants { get; set; }

        [Required(ErrorMessage = "Minimum 1 lineup Maximum 150")]
        [Range(1, 150)]
        [Display(Name = "How many 7-man Lineups do you want to build?")]
        public int NumberOfRosters { get; set; }

        [Required(ErrorMessage = "The max salary is $100,000 on FantasyDraft")]
        [Range(90500, 100000)]
        [Display(Name = "What's the max salary you want used for your rosters?")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "Please use at least $88,000 of the available Salary")]
        [Range(88000, 99900)]
        [Display(Name = "What's the salary floor for your rosters?")]
        public int MinSalary { get; set; }

        public DisplayFDraftTourneySalariesViewModel()
        {
            // List<Golfer> DKParticipants = new List<Golfer>();
            TourneyParticipants = new List<FDraftGolfer>();
            FDraftName = new FDraftTourney();

            NumberOfRosters = 1;
            MaxSalary = 100000;
            MinSalary = 88000;
        }

        public DisplayFDraftTourneySalariesViewModel(FDraftTourney fdt, List<FDraftGolfer> fdtGolfers)
        {
            TourneyParticipants = new List<FDraftGolfer>();

            foreach (var golfer in fdtGolfers)
            {
                TourneyParticipants.Add(golfer);

            }

            NumberOfRosters = 1;
            MaxSalary = 100000;
            MinSalary = 88000;
            FDraftName = fdt;

        }
    }
}
