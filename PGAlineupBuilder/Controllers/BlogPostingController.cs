using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Data;

namespace PGAlineupBuilder.Controllers
{
    public class BlogPostingController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public BlogPostingController(PGAlineupBuilderDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Last5Blogs()
        {
           // IList < BlogPost > = context.BP.Where(b => b.name == "").ToList<BlogPost>();

            return View("BlogView");
        }

        public IActionResult SearchBlogs(string sTerm)
        {
            if(!string.IsNullOrWhiteSpace(sTerm))
            {
               // IList<BlogPost> searchPost = context.BP.Where(b => b.name.contains(sTerm)).ToList<BlogPost>();

               // if(searchPost.Count() = 0)
             //   {
              //      ViewBag.Nothing = $"Sorry, no Blogs with {sTerm} in the title were found";
               //     return View("Index");
              //  }
               // else
              //  {
                    return View("SearchResults");
              //  }
                
            }
            else
            {
                
                return View("Index");
            }
        }
    }
}
