using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Data;
using PGAlineupBuilder.ViewModels;
using System.Collections;

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

        public IActionResult NewPost()
        {
            //Query the Database for existing Tags and Categories to pass to the View Model
            IList<Category> cats = context.BPCAT.ToList<Category>();
            IList<Tag> tags = context.BPTag.ToList<Tag>();

            NewBlogPostViewModel createBlog = new NewBlogPostViewModel(tags, cats);
            return View("NewPost",createBlog);
        }

        [HttpPost]
        public IActionResult PublishPost(NewBlogPostViewModel createBlog)
        {

           return View();
        }

        public IActionResult Last5Blogs()
        {
            IList<BlogPost> blogs = context.BP.ToList<BlogPost>();

            return View("BlogView",blogs);
        }

        public IActionResult SearchBlogs(string sTerm)
        {
            if(!string.IsNullOrWhiteSpace(sTerm))
            {
                IList<BlogPost> searchPost = context.BP.Where(b => b.Name == sTerm).ToList<BlogPost>();

                if(searchPost.Count() == 0)
                {
                    ViewBag.Nothing = $"Sorry, no Blogs with {sTerm} in the title were found";
                   return View("Index");
                }
                else
                {
                    return View("SearchResults");
                }
                
            }
            else
            {
                
                return View("Index");
            }
        }
    }
}
