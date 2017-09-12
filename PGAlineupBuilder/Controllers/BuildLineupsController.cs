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
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PGAlineupBuilder.Controllers
{
    [Authorize(Roles="PGAguru, Member")]
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
            var fdTourneys = context.FDT.ToList();

            return View(fdTourneys);
        }

        public IActionResult ChooseFDraft()
        {
            var fDraftTourneys = context.FDraftT.ToList();

            return View(fDraftTourneys);
        }

        //Take selected DkTourney and return a newly created DisplayTourneySalariesViewModel
        public IActionResult getDK(string DKselected)
        {
            DkTourney dkSelected = context.DKT.Single(s => s.Name == DKselected);

            int DKTid = dkSelected.ID;

            List<Golfer> selectedGolfers = context.GOLFER.Where(s => s.DkTourneyID == DKTid).ToList<Golfer>();


            DisplayTourneySalariesViewModel model = new DisplayTourneySalariesViewModel(dkSelected, selectedGolfers)
            {

            };

            return View("DisplayDK", model);
        }

        public IActionResult getFD(string FDselected)
        {
            var fdSelection = context.FDT.Single(s => s.Name == FDselected);

            int fdID = fdSelection.ID;

            List<FDgolfer> selectedFDgolfers = context.FDGOLFER.Where(i => i.FDtourneyID == fdID).ToList<FDgolfer>();

            DisplayFDtourneySalariesViewModel model = new DisplayFDtourneySalariesViewModel(fdSelection, selectedFDgolfers)
            {

            };

            return View("DisplayFD", model);


        }

        public IActionResult getFDraft(string FDraftSelected)
        {
            var fdraftSelection = context.FDraftT.Single(s => s.Name == FDraftSelected);

            int fdID = fdraftSelection.ID;

            List<FDraftGolfer> selectedFDraftGolfers = context.FDraftG.Where(i => i.FDraftTourneyID == fdID).ToList<FDraftGolfer>();

            DisplayFDraftTourneySalariesViewModel model = new DisplayFDraftTourneySalariesViewModel(fdraftSelection, selectedFDraftGolfers)
            {

            };

            return View("DisplayFDraft", model);

        }

        public IActionResult DisplayDK(DisplayTourneySalariesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dkTourneyName = context.DKT.Single(s => s.Name == model.DKname.Name);
                string TourneyBuildName = dkTourneyName.Name;
                IList<Golfer> currentGolfers = model.TourneyParticipants;

                //Get a list of Golfers with > 0 Exposure
                IList<Golfer> ExposureGolfers = currentGolfers.Where(s => s.Exposure > 0).ToList();

                if (ExposureGolfers.Count() < 10)
                {
                    ViewBag.TenNeeded = "Please select Exposure to at least 10 Golfers";
                    return View(model);
                }


                int numbaLineups = model.NumberOfRosters;
                int maxS = model.MaxSalary;
                int minS = model.MinSalary;

                //adjust Exposure %s for each Golfer specified from the View if Lineup amount > 5
                if (numbaLineups > 5)
                {
                    foreach (var golfer in ExposureGolfers)
                    {
                        double GolferPercentage = (golfer.Exposure / 100);
                        golfer.Exposure = Math.Round((GolferPercentage * numbaLineups), MidpointRounding.AwayFromZero);

                    }

                }

                List<Golfer> NewExposureSorted = ExposureGolfers.OrderByDescending(g => g.Exposure).ToList();

                List<DKlineup> generatedLineups = new List<DKlineup>();

                int lineupcounter = 0;
                //try
                // {
                //begin building lineups according to # from numbaLineups
                for (var l = 0; l < numbaLineups; l++)
                {

                    DKlineup newLineup = new DKlineup
                    {
                        LineupID = lineupcounter,
                    };

                    int attemptCounter = 0;

                    List<Golfer> DuplicateList = new List<Golfer>();

                    //try to create 6 man Lineup while falling into specified Salary Usage Range
                    while (newLineup.LineupGolfers.Count() < 6)
                    {


                        Golfer chosenGolfer = NewExposureSorted.FirstOrDefault(s => s.Exposure > 0);
                        if (chosenGolfer == null)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);
                        }

                        //keep chosing random golfers from ExposureGolfers list until one is found that is not in the DuplicateList
                        while ((DuplicateList.Contains(chosenGolfer)) || ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {

                            attemptCounter++;

                            var rando = new Random();
                            chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];



                            if (chosenGolfer.Exposure <= 0)
                            {
                                chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];
                            }

                            if (attemptCounter == 9999)
                            {
                                break;

                            }

                            System.Threading.Thread.Sleep(5);

                        }

                        if (attemptCounter == 9999)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);

                        }

                        if (newLineup.LineupGolfers.Count() == 2 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 19500)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (Golfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }

                        if (newLineup.LineupGolfers.Count() == 3 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 13000)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (Golfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }

                        if (newLineup.LineupGolfers.Count() == 4 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 6100)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (Golfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
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
                        else if (newLineup.Lineup.Count() == 6 && ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else
                        {
                            //add golfer to Lineup and Increase runningSalary amount and newLineup.amount
                            newLineup.Lineup.Add(chosenGolfer.Playerid);
                            newLineup.LineupSalary += chosenGolfer.Salary;
                            chosenGolfer.Exposure--;
                            newLineup.amount_golfers++;
                            DuplicateList.Add(chosenGolfer);
                        }


                    }

                    //add Newly created Lineup to list of lineups and increment the lineup count
                    if (generatedLineups.Contains(newLineup))
                    {
                        ViewBag.Duplicates = "Sorry, your Build contained Duplicate Lineups...try again";
                        return View(model);
                    }

                    generatedLineups.Add(newLineup);
                    lineupcounter++;

                }

                BuiltDKLineupsViewModel yourCreatedLineups = new BuiltDKLineupsViewModel(dkTourneyName, generatedLineups)
                {

                };
                //ViewBag.Success = generatedLineups;
                //ViewBag.tourneyname = dkTourneyName;
                return View("BuiltDK", yourCreatedLineups);

                // }
                // catch(Exception e)
                // {


                //  ViewBag.Exception = $"{e.Message}";
                //  return View(model);

                //}

            }
            else
            {
               

                return View(model);

            }


        }
        [HttpPost]
        public FileStreamResult ExportLineupsCSV(BuiltDKLineupsViewModel yourCreatedLineups)
        {
            MemoryStream memoryS = new MemoryStream();
            StreamWriter streamW = new StreamWriter(memoryS);


            var namer = yourCreatedLineups.BuiltDK.Name;
            var lineups = yourCreatedLineups.listDKlineups.ToList();




            foreach (DKlineup lineup in lineups)
            {
                foreach (Golfer golferr in lineup.LineupGolfers)
                {
                    streamW.Write(String.Format("{0},", golferr.Playerid));

                }
                streamW.WriteLine();
            }

            streamW.Flush();
            streamW.BaseStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(streamW.BaseStream, "text/csv") { FileDownloadName = $"{namer}.csv" };

        }

        public IActionResult DisplayFD(DisplayFDtourneySalariesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fdTourneyName = context.FDT.Single(s => s.Name == model.FDname.Name);
                string TourneyBuildName = fdTourneyName.Name;
                IList<FDgolfer> currentGolfers = model.TourneyParticipants;

                //Get a list of Golfers with > 0 Exposure
                IList<FDgolfer> ExposureGolfers = currentGolfers.Where(s => s.Exposure > 0).ToList();

                if (ExposureGolfers.Count() < 12)
                {
                    ViewBag.TenNeeded = "Please select Exposure to at least 12 Golfers";
                    return View(model);
                }


                int numbaLineups = model.NumberOfRosters;
                int maxS = model.MaxSalary;
                int minS = model.MinSalary;

                //adjust Exposure %s for each Golfer specified from the View if Lineup amount > 5
                if (numbaLineups > 5)
                {
                    foreach (var golfer in ExposureGolfers)
                    {
                        double GolferPercentage = (golfer.Exposure / 100);
                        golfer.Exposure = Math.Round((GolferPercentage * numbaLineups), MidpointRounding.AwayFromZero);

                    }

                }

                List<FDgolfer> NewExposureSorted = ExposureGolfers.OrderByDescending(g => g.Exposure).ToList();



                List<FDlineup> generatedLineups = new List<FDlineup>();

                int lineupcounter = 0;
                //try
                // {
                //begin building lineups according to # from numbaLineups
                for (var l = 0; l < numbaLineups; l++)
                {

                    FDlineup newLineup = new FDlineup
                    {
                        LineupID = lineupcounter,
                    };

                    int attemptCounter = 0;

                    List<FDgolfer> DuplicateList = new List<FDgolfer>();

                    //try to create 6 man Lineup while falling into specified Salary Usage Range
                    while (newLineup.LineupGolfers.Count() < 8)
                    {


                        FDgolfer chosenGolfer = NewExposureSorted.FirstOrDefault(s => s.Exposure > 0);
                        if (chosenGolfer == null)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);
                        }


                        //keep chosing random golfers from ExposureGolfers list until one is found that is not in the DuplicateList
                        while ((DuplicateList.Contains(chosenGolfer)) || ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {

                            attemptCounter++;

                            var rando = new Random();
                            chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];



                            if (chosenGolfer.Exposure <= 0)
                            {
                                chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];
                            }

                            if (attemptCounter == 9999)
                            {
                                break;


                            }

                            System.Threading.Thread.Sleep(5);

                        }

                        if (attemptCounter == 9999)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);

                        }

                        if (newLineup.LineupGolfers.Count() == 4 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 15600)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDgolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }

                        if (newLineup.LineupGolfers.Count() == 5 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 10000)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDgolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }

                        if (newLineup.LineupGolfers.Count() == 6 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 4600)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDgolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }


                        newLineup.LineupGolfers.Add(chosenGolfer);


                        //Remove Golfer if Golfer is 6th Golfer in the Lineup and minSalary hasn't been met and start WHILE LOOP over
                        if (newLineup.LineupGolfers.Count() == 8 && ((newLineup.LineupSalary + chosenGolfer.Salary) < minS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else if (newLineup.Lineup.Count() == 8 && ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else
                        {
                            //add golfer to Lineup and Increase runningSalary amount and newLineup.amount
                            newLineup.Lineup.Add(chosenGolfer.Playerid);
                            newLineup.LineupSalary += chosenGolfer.Salary;
                            chosenGolfer.Exposure--;
                            newLineup.amount_golfers++;
                            DuplicateList.Add(chosenGolfer);
                        }


                    }

                    //add Newly created Lineup to list of lineups and increment the lineup count
                    if (generatedLineups.Contains(newLineup))
                    {
                        ViewBag.Duplicates = "Sorry, your Build contained Duplicate Lineups...try again";
                        return View(model);
                    }

                    generatedLineups.Add(newLineup);
                    lineupcounter++;

                }

                BuiltFDLineupsViewModel yourCreatedLineups = new BuiltFDLineupsViewModel(fdTourneyName, generatedLineups)
                {

                };
              
                return View("BuiltFD", yourCreatedLineups);

                // }
                // catch(Exception e)
                // {


                //  ViewBag.Exception = $"{e.Message}";
                //  return View(model);

                //}



            }
            else
            {

                return View(model);

            }
        }


        [HttpPost]
        public FileStreamResult ExportFDLineupsCSV(BuiltFDLineupsViewModel yourCreatedLineups)
        {
            MemoryStream memoryS = new MemoryStream();
            StreamWriter streamW = new StreamWriter(memoryS);


            var namer = yourCreatedLineups.BuiltFD.Name;
            var lineups = yourCreatedLineups.listFDlineups.ToList();




            foreach (FDlineup lineup in lineups)
            {
                foreach (FDgolfer golferr in lineup.LineupGolfers)
                {
                    streamW.Write(String.Format("{0},", golferr.Playerid));

                }
                streamW.WriteLine();
            }

            streamW.Flush();
            streamW.BaseStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(streamW.BaseStream, "text/csv") { FileDownloadName = $"{namer}.csv" };

        }

        public IActionResult DisplayFDraft(DisplayFDraftTourneySalariesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fdraftTourneyName = context.FDraftT.Single(s => s.Name == model.FDraftName.Name);
                string TourneyBuildName = fdraftTourneyName.Name;
                IList<FDraftGolfer> currentGolfers = model.TourneyParticipants;

                //Get a list of Golfers with > 0 Exposure
                IList<FDraftGolfer> ExposureGolfers = currentGolfers.Where(s => s.Exposure > 0).ToList();

                if (ExposureGolfers.Count() < 12)
                {
                    ViewBag.TenNeeded = "Please select Exposure to at least 12 Golfers";
                    return View(model);
                }


                int numbaLineups = model.NumberOfRosters;
                int maxS = model.MaxSalary;
                int minS = model.MinSalary;

                //adjust Exposure %s for each Golfer specified from the View if Lineup amount > 5
                if (numbaLineups > 5)
                {
                    foreach (var golfer in ExposureGolfers)
                    {
                        double GolferPercentage = (golfer.Exposure / 100);
                        golfer.Exposure = Math.Round((GolferPercentage * numbaLineups), MidpointRounding.AwayFromZero);

                    }

                }

                List<FDraftGolfer> NewExposureSorted = ExposureGolfers.OrderByDescending(g => g.Exposure).ToList();



                List<FDraftLineup> generatedLineups = new List<FDraftLineup>();

                int lineupcounter = 0;
                //try
                // {
                //begin building lineups according to # from numbaLineups
                for (var l = 0; l < numbaLineups; l++)
                {

                    FDraftLineup newLineup = new FDraftLineup
                    {
                        LineupID = lineupcounter,
                    };

                    int attemptCounter = 0;

                    List<FDraftGolfer> DuplicateList = new List<FDraftGolfer>();

                    //try to create 6 man Lineup while falling into specified Salary Usage Range
                    while (newLineup.LineupGolfers.Count() < 7)
                    {


                        FDraftGolfer chosenGolfer = NewExposureSorted.FirstOrDefault(s => s.Exposure > 0);
                        if (chosenGolfer == null)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);
                        }


                        //keep chosing random golfers from ExposureGolfers list until one is found that is not in the DuplicateList
                        while ((DuplicateList.Contains(chosenGolfer)) || ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {

                            attemptCounter++;

                            var rando = new Random();
                            chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];



                            if (chosenGolfer.Exposure <= 0)
                            {
                                chosenGolfer = NewExposureSorted[rando.Next(NewExposureSorted.Count)];
                            }

                            if (attemptCounter == 99999)
                            {
                                break;


                            }

                            System.Threading.Thread.Sleep(5);

                        }

                        if (attemptCounter == 9999)
                        {
                            ViewBag.Attempts = "Please Adjust your settings(select more Golfer Exposures) and try to Build again.";
                            return View(model);

                        }

                        if (newLineup.LineupGolfers.Count() == 4 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 23500)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDraftGolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }

                        if (newLineup.LineupGolfers.Count() == 5 && ((newLineup.LineupSalary + chosenGolfer.Salary) > (maxS - 10800)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDraftGolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }
                        else if (newLineup.LineupGolfers.Count() == 5 && ((newLineup.LineupSalary + chosenGolfer.Salary) < (minS - 17000)))
                        {
                            //Increase the Exposure for each Golfer in the LineupGolfers since we're putting it back into selection pool
                            foreach (FDraftGolfer golfa in newLineup.LineupGolfers)
                            {
                                golfa.Exposure++;
                            }
                            newLineup.LineupGolfers.Clear();
                            newLineup.LineupSalary = 0;
                            newLineup.amount_golfers = 0;
                            newLineup.Lineup.Clear();
                            DuplicateList.Clear();
                        }
                        else
                        {
                            newLineup.LineupGolfers.Add(chosenGolfer);
                        }
                        


                        //Remove Golfer if Golfer is 7th Golfer in the Lineup and minSalary hasn't been met and start WHILE LOOP over
                        if (newLineup.LineupGolfers.Count() == 7 && ((newLineup.LineupSalary + chosenGolfer.Salary) < minS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else if (newLineup.Lineup.Count() == 7 && ((newLineup.LineupSalary + chosenGolfer.Salary) > maxS))
                        {
                            newLineup.LineupGolfers.Remove(chosenGolfer);
                        }
                        else
                        {
                            //add golfer to Lineup and Increase runningSalary amount and newLineup.amount
                            newLineup.Lineup.Add(chosenGolfer.Playerid);
                            newLineup.LineupSalary += chosenGolfer.Salary;
                            chosenGolfer.Exposure--;
                            newLineup.amount_golfers++;
                            DuplicateList.Add(chosenGolfer);
                        }


                    }

                    //add Newly created Lineup to list of lineups and increment the lineup count
                    if (generatedLineups.Contains(newLineup))
                    {
                        ViewBag.Duplicates = "Sorry, your Build contained Duplicate Lineups...try again";
                        return View(model);
                    }

                    generatedLineups.Add(newLineup);
                    lineupcounter++;

                }

                BuiltFDraftLineupsViewModel yourCreatedLineups = new BuiltFDraftLineupsViewModel(fdraftTourneyName, generatedLineups)
                {

                };
             
                return View("BuiltFDraft", yourCreatedLineups);

            
            }
            else
            {

                return View(model);

            }
        }

        [HttpPost]
        public FileStreamResult ExportFDraftLineupsCSV(BuiltFDraftLineupsViewModel yourCreatedLineups)
        {
            MemoryStream memoryS = new MemoryStream();
            StreamWriter streamW = new StreamWriter(memoryS);


            var namer = yourCreatedLineups.BuiltFDraft.Name;
            var lineups = yourCreatedLineups.listFDraftLineups.ToList();




            foreach (FDraftLineup lineup in lineups)
            {
                foreach (FDraftGolfer golferr in lineup.LineupGolfers)
                {
                    streamW.Write(String.Format("{0},", golferr.Playerid));

                }
                streamW.WriteLine();
            }

            streamW.Flush();
            streamW.BaseStream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(streamW.BaseStream, "text/csv") { FileDownloadName = $"{namer}.csv" };

        }












    }

}

