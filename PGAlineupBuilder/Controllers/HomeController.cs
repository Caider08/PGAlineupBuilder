using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;

namespace PGAlineupBuilder.Controllers
{
    public class HomeController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public HomeController(PGAlineupBuilderDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IActionResult Index()
        {
            IList<BlogPost> recentPosts = context.BP.OrderByDescending(bp => bp.PublishedDate).Take(5).ToList<BlogPost>();
           // foreach(BlogPost post in recentPosts)
          //  {
          //      post.Tag = context.BPTag.Single(p => p.ID == post.Tag.ID);
          //      post.Category = context.BPCAT.Single(c => c.ID == post.Category.ID);

          //  }
            return View(recentPosts);
        }

        public IActionResult About()
        {
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
