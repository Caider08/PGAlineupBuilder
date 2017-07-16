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


        public BuildLineupsController(PGAlineupBuilderDbContext dbContext)
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

        
        public IActionResult getDK(string DKselected)
        {
            DkTourney dkSelected = context.DKT.Single(s => s.Name == DKselected);

            int DKTid = dkSelected.ID;

            List<Golfer> selectedGolfers = context.GOLFER.Where(s => s.DkTourneyID == DKTid).ToList<Golfer>();

           // TempData["dkSELECTED"] = new DkTourney() { Name = dkSelected.Name, Participants = dkSelected.Participants };
           // selectedGolfers = (List<Golfer>)TempData["dkGOLFERS"];

            //TempData.Keep("dkSELECTED");
           // TempData.Keep("dkGOLFERS");


             DisplayTourneySalariesViewModel model = new DisplayTourneySalariesViewModel(dkSelected, selectedGolfers)
            {

            };

            //ViewBag.SelectedDKT = dkSelected;
            //ViewBag.SelectedDKgolfers = selectedGolfers;
            return View("DisplayDK", model);
        }

        [HttpPost]
        public IActionResult DisplayDK(DisplayTourneySalariesViewModel model)
        {
            if(ModelState.IsValid)
            {
                var dkTourneyName = context.DKT.Single(s => s.Name == model.DKname.Name);
                int numbaLineups = model.NumberOfRosters;
                int maxS = model.MaxSalary;
                int minS = model.MaxSalary;

                ViewBag.Success = dkTourneyName.Name;
                return View("BuiltDK");

            }
            else
            {
                //if (model.NumberOfRosters < 1 || model.NumberOfRosters > 150)
               // {
               //     ViewBag.RosterError = "Please Build between 1-150 lineups";
               //     return View(model);
               // }
               // else if (model.MinSalary < 38500 || model.MinSalary > 50000)
               // {
                   // ViewBag.MinError = "Please choose between 38,500 and 50,000 for Salary Floor";
                   // return View(model);
               // }
               // else if (model.MaxSalary < 38500 || model.MaxSalary > 50000)
               // {
                    //ViewBag.MaxError = "Please choose between 38,500 and 50,000 for Salary Max";
                   // return View(model);
                //}
                //else if (model.MinSalary > model.MaxSalary)
               // {
               //     ViewBag.SalaryError = "Max Salary must be greater than Salary Floor";
               //     return View(model);
               // }
               
                return View(model);
                
            }
            



        }
           

            

        
           // string dkTourneyName = golfers.First().GameInfo;

            //if (Rosters < 1 || Rosters > 150)
            //{
           //     ViewBag.SelectedDKT = dkTourneyName;
           //     ViewBag.RosterError = "Please build between 1-150 lineups";
            //    return View("DisplayDK", golfers);
           // }
            

           
            //var tourney = model.Name;
            //string dkselected = tourney.Name;
          
           // IEnumerable<Golfer> displaysGolfers = incomingDisplay.Participants.ToList();
           // DkTourney displaysTourney = incomingDisplay.Name;

            //if (ModelState.IsValid)
           // {
               // int numbaLineups = model.NumberOfRosters;
               // int maxS = model.MaxSalary;
                //int minS = model.MaxSalary;



                //return View("BuildDK");
            //}

            // DisplayTourneySalariesViewModel anotherDisplayDKT = new DisplayTourneySalariesViewModel(displaysTourney, displaysGolfers)
            // {

            //  };
        
            //return RedirectToAction("getDK", "BuildLineups", new { id = dkselected });
        
    }
}
