using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using System.Net;


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
