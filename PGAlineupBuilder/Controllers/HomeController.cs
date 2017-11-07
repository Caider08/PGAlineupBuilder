using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace PGAlineupBuilder.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
      
        private PGAlineupBuilderDbContext context;

        public HomeController(PGAlineupBuilderDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IActionResult Index()
        {
            IList<BlogPost> recentPosts = context.BP.Include(bp => bp.Tag).Include(bp => bp.Category).OrderByDescending(bp => bp.PublishedDate).Take(6).ToList<BlogPost>();
           
            return View(recentPosts);
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Fantasy Golf Masters";
            ViewData["Message"] = "Website and Database built and managed by cbDev";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
