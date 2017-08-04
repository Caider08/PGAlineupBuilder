using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGAlineupBuilder.Controllers
{
    public class GolfersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchTerm, string SearchMethod)
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

               }

                return View("SearchResults");
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
