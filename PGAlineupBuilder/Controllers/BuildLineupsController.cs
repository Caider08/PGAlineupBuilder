using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Data;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Hosting;
using PGAlineupBuilder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PGAlineupBuilder.Controllers
{
    public class BuildLineupsController : Controller
    {
        private PGAlineupBuilderDbContext context;


        public BuildLineupsController( PGAlineupBuilderDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseDK()
        {
            var DKTourneys = context.DKT.ToList();

            return View(DKTourneys);
        }

        public IActionResult ChooseFD()
        {
           // List<Golfer> testGolfers = new List<Golfer>();

            string testGolf = "The Greenbrier Classic 2017";

            var tourney = context.DKT.Single(s => s.Name == testGolf);

            int Tid = tourney.ID;

            var testGolfers = context.GOLFER.Where(c => c.DkTourneyID == Tid).ToList<Golfer>();

            

            ViewBag.TestTourney = tourney;
            ViewBag.ListTest = testGolfers;

            return View();
        }

        public IActionResult DisplayDK(string DKselected)
        {
            DkTourney dkSelected = context.DKT.Single(s => s.Name == DKselected);

            List<Golfer> selectedGolfers = new List<Golfer>();

            foreach(Golfer golfer in dkSelected.Participants)
            {
                selectedGolfers.Add(golfer);
            }
            

            DisplayTourneySalariesViewModel DisplayDKT = new DisplayTourneySalariesViewModel(dkSelected, selectedGolfers);
            

            return View(DisplayDKT);
        }

    }
}
