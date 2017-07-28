﻿using System;
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

        //get all the DkTourneys from the Database and display in View
        public IActionResult ChooseDK()
        {
            var DKTourneys = context.DKT.ToList();

            return View(DKTourneys);
        }

        //just a Test...FanDuel part of site not built / implemented yet.
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

        //Take selected DkTourney and return a newly created DisplayTourneySalariesViewModel
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
                IList<Golfer> currentGolfers = model.TourneyParticipants;

                //Get a list of Golfers with > 0 Exposure
                IList<Golfer> ExposureGolfers = currentGolfers.Where(s => s.Exposure > 0).ToList();
                
           
                int numbaLineups = model.NumberOfRosters;
                int maxS = model.MaxSalary;
                int minS = model.MaxSalary;

                //adjust Exposure %s for each Golfer specified from the View
                foreach(var golfer in ExposureGolfers)
                {
                    double GolferPercentage = (golfer.Exposure / 100);
                    golfer.Exposure = Math.Round((GolferPercentage * numbaLineups), 0);

                }

                List<DKlineup> generatedLineups = new List<DKlineup>();

                int lineupcounter = 0;

                //beging building lineups according to # from numbaLineups
                for (var l=0; l < numbaLineups; l++)
                 {
                    
                    DKlineup newLineup = new DKlineup
                    {
                        LineupID = lineupcounter,
                    };

                    List<Golfer> DuplicateList = new List<Golfer>();

                    //try to create 6 man Lineup while falling into specified Salary Usage Range
                    while (newLineup.LineupGolfers.Count() < 7)
                    {
                      
                        Golfer chosenGolfer = ExposureGolfers.First(s => s.Exposure >= 0);

                        //keep chosing random golfers from ExposureGolfers list until one is found that is not in the DuplicateList
                        while (DuplicateList.Contains(chosenGolfer) || ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {
                            var rando = new Random();
                            chosenGolfer = ExposureGolfers[rando.Next(ExposureGolfers.Count)];
                            if (chosenGolfer.Exposure <= 0)
                            {
                                chosenGolfer = ExposureGolfers[rando.Next(ExposureGolfers.Count)];
                            }
                        }

                        newLineup.LineupGolfers.Add(chosenGolfer);

                        //keep re-selecting a golfer until one fits within the Max Salary range
                       // while ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS)
                      //  {
                      //      newLineup.LineupGolfers.Remove(chosenGolfer);
                      //      chosenGolfer = currentGolfers.First(s => s.Exposure >= 0);
                      //      newLineup.LineupGolfers.Add(chosenGolfer);
                      //  }

                        

                        //Remove Golfer if Golfer is 6th Golfer in the Lineup and minSalary hasn't been met and start WHILE LOOP over
                        if (newLineup.LineupGolfers.Count() == 6 && ((newLineup.LineupSalary + chosenGolfer.Salary) < minS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        if (newLineup.Lineup.Count() == 6 && ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else
                        {
                            //add golfer to Lineup and Increase runningSalary amount and newLineup.amount
                            newLineup.Lineup.Add(chosenGolfer.Playerid);
                            newLineup.LineupSalary += chosenGolfer.Salary;
                            chosenGolfer.Exposure -= 1;
                            newLineup.amount_golfers += 1;
                            DuplicateList.Add(chosenGolfer);
                        }
                      
                    

                    }

                    //add Newly created Lineup to list of lineups and increment the lineup count
                    generatedLineups.Add(newLineup);
                    lineupcounter++;
                    
                 }

                ViewBag.Success = generatedLineups;
                ViewBag.Tname = dkTourneyName;
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
