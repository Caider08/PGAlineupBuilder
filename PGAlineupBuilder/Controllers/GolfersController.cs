﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using System.Net;
using System.Text.RegularExpressions;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGAlineupBuilder.Controllers
{
    public class GolfersController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public GolfersController(PGAlineupBuilderDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchD(string searchTerm, string SearchMethod)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                ViewBag.BlankTerm = "Please enter a search word";
                return View("Index");
            }
            else
            {
                if (!Regex.IsMatch(searchTerm, @"^[a-zA-Z'0-9\s.-]{1,80}$"))
                {
                    ViewBag.None = "Invalid Characters in Search";
                    return View("Index");
                }

                if (SearchMethod == "ByGolfer")
                {
                    List<Golfer> returnedGolfer = context.GOLFER.Where(g => g.Name.Contains(searchTerm)).ToList();
                    if (returnedGolfer.Count() < 1)
                    {
                        ViewBag.None = "Your Search didn't return any results";
                        return View("Index");
                    }

                    Golfer golferExample = returnedGolfer.First();
                    var tourneysearch = context.DKT.Single(t => t.Name.Contains(golferExample.GameInfo));
                    ViewBag.Tourney = tourneysearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResults");
                }
                else if(SearchMethod == "ByTournament")
                {
                    var tourneySearch = context.DKT.Where(t => t.Name.Contains(searchTerm)).FirstOrDefault();
                    if (tourneySearch == null)
                    {
                        ViewBag.None = "Your Search didn't return any results";
                        return View("Index");
                    }

                    List<Golfer> returnedGolfer = context.GOLFER.Where(g => g.GameInfo.Contains(tourneySearch.Name)).ToList<Golfer>();
                    //List<Golfer> returnedGolfer = tourneySearch.Participants.ToList<Golfer>();
                    ViewBag.Tourney = tourneySearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResults");
                }
                else
                {
                    ViewBag.None = "Your Search didn't return any results";
                    return View("Index");
                }
               

               
            }
          
        }

        [HttpPost]
        public IActionResult SearchFD(string searchTermFD, string SearchMethodFD)
        {
            if (string.IsNullOrWhiteSpace(searchTermFD))
            {
                ViewBag.BlankTermFD = "Please enter a search word";
                return View("Index");
            }
            else
            {
                if (!Regex.IsMatch(searchTermFD, @"^[a-zA-Z'0-9\s.-]{1,80}$"))
                {
                    ViewBag.None = "Invalid Characters in Search";
                    return View("Index");
                }

                if (SearchMethodFD == "ByGolfer")
                {
                    List<FDgolfer> returnedGolfer = context.FDGOLFER.Where(g => g.Name.Contains(searchTermFD)).ToList();
                    if (returnedGolfer.Count() < 1)
                    {
                        ViewBag.NoneFD = "Your Search didn't return any results";
                        return View("Index");
                    }

                    FDgolfer golferExample = returnedGolfer.First();
                    var tourneysearch = context.FDT.Single(t => t.Name.Contains(golferExample.GameInfo));
                    ViewBag.Tourney = tourneysearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResultsFD");
                }
                else if (SearchMethodFD == "ByTournament")
                {
                    var tourneySearch = context.FDT.Where(t => t.Name.Contains(searchTermFD)).FirstOrDefault();
                    if (tourneySearch == null)
                    {
                        ViewBag.NoneFD = "Your Search didn't return any results";
                        return View("Index");
                    }

                    List<FDgolfer> returnedGolfer = context.FDGOLFER.Where(g => g.GameInfo.Contains(tourneySearch.Name)).ToList<FDgolfer>();
                    //List<Golfer> returnedGolfer = tourneySearch.Participants.ToList<Golfer>();
                    ViewBag.Tourney = tourneySearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResultsFD");
                }
                else
                {
                    ViewBag.NoneFD = "Your Search didn't return any results";
                    return View("Index");
                }



            }

        }

        [HttpPost]
        public IActionResult SearchFDraft(string searchTermFDraft, string SearchMethodFDraft)
        {
            if (string.IsNullOrWhiteSpace(searchTermFDraft))
            {
                ViewBag.BlankTermFDraft = "Please enter a search word";
                return View("Index");
            }
            else
            {
                if (!Regex.IsMatch(searchTermFDraft, @"^[a-zA-Z'0-9\s.-]{1,80}$"))
                {
                    ViewBag.NoneFDraft = "Invalid Characters in Search";
                    return View("Index");
                }

                if (SearchMethodFDraft == "ByGolfer")
                {
                    List<FDraftGolfer> returnedGolfer = context.FDraftG.Where(g => g.Name.Contains(searchTermFDraft)).ToList();
                    if (returnedGolfer.Count() < 1)
                    {
                        ViewBag.NoneFDraft = "Your Search didn't return any results";
                        return View("Index");
                    }

                    FDraftGolfer golferExample = returnedGolfer.First();
                    var tourneysearch = context.FDraftT.Single(t => t.Name.Contains(golferExample.GameInfo));
                    ViewBag.Tourney = tourneysearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResultsFDraft");
                }
                else if (SearchMethodFDraft == "ByTournament")
                {
                    var tourneySearch = context.FDT.Where(t => t.Name.Contains(searchTermFDraft)).FirstOrDefault();
                    if (tourneySearch == null)
                    {
                        ViewBag.NoneFDraft = "Your Search didn't return any results";
                        return View("Index");
                    }

                    List<FDraftGolfer> returnedGolfer = context.FDraftG.Where(g => g.GameInfo.Contains(tourneySearch.Name)).ToList<FDraftGolfer>();
                    //List<Golfer> returnedGolfer = tourneySearch.Participants.ToList<Golfer>();
                    ViewBag.Tourney = tourneySearch;
                    ViewBag.GolferResults = returnedGolfer;
                    return View("SearchResultsFDraft");
                }
                else
                {
                    ViewBag.NoneFDraft = "Your Search didn't return any results";
                    return View("Index");
                }



            }
        }

        [HttpPost]
        public IActionResult DraftKings()
        {
            //return Redirect("DraftKingsRoster");
            return View("DraftKingsRoster");
        }

        [HttpPost]
        public IActionResult FanDuel()
        {
            return View("FanDuelRoster");
        }
    }
}
