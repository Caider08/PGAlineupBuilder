using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace PGAlineupBuilder.ViewModels
{
    public class DisplayTourneySalariesViewModel
    {
        public DkTourney DKname { get; set; }

        public IList<Golfer> TourneyParticipants { get; set; }

        [Required(ErrorMessage = "Minimum 1 lineup Maximum 150")]
        [Range(1,150)]
        [Display(Name ="How many 6-man Lineups do you want to build?")]
        public int NumberOfRosters { get; set; }

        [Required(ErrorMessage = "The max salary is $50,000 on DraftKings")]
        [Range(39000,50000)]
        [Display(Name="What's the max salary you want used for your rosters?")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "Please use at least $38,500 of the available Salary")]
        [Range(38500, 50000)]
        [Display(Name="What's the salary floor for your rosters?")]
        public int MinSalary { get; set; }

        public DisplayTourneySalariesViewModel()
        {
            // List<Golfer> DKParticipants = new List<Golfer>();
            TourneyParticipants = new List<Golfer>();
            DKname = new DkTourney();

            NumberOfRosters = 1;
            MaxSalary = 50000;
            MinSalary = 38500;
        }

        public DisplayTourneySalariesViewModel(DkTourney dkt, List<Golfer> dktGolfers)
        {
            TourneyParticipants = new List<Golfer>();
            
            foreach (var golfer in dktGolfers)
            {
                TourneyParticipants.Add(golfer);
               
            }

            NumberOfRosters = 1;
            MaxSalary = 50000;
            MinSalary = 38500;
            DKname = dkt;
           
        }


            



    }
}
