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
            IList<BlogPostTag> tags = context.BPostTag.ToList<BlogPostTag>();

            NewBlogPostViewModel createBlog = new NewBlogPostViewModel(tags, cats);
            return View("NewPost",createBlog);
        }

        [HttpPost]
        public IActionResult PublishPost(NewBlogPostViewModel createBlog)
        {
            if (ModelState.IsValid)
            {
                Category newBlogCategory = context.BPCAT.Single(c => c.ID == createBlog.CategoryID);

                Tag newBlogTag = context.BPTag.Single(c => c.ID == createBlog.TagID);

                BlogPost bpost = new BlogPost()
                {
                    Name = createBlog.Name,
                    Content = createBlog.content,
                    PublishedDate = DateTime.Now,
                    Meta = createBlog.meta,
                    URLslug = createBlog.urlSlug,

                    Category = newBlogCategory,
                    Tag = newBlogTag,

                };

                context.BP.Add(bpost);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
                
            }

            return View();
           
        }

        [HttpGet]
        public IActionResult NewCategory()
        {
            return View("AddCategory");
        }
        
        [HttpPost]
        public IActionResult CreateCategory(string name, string description, string urlSLUG)
        {
            Category newCat = new Category()
            {
                Name = name,
                Description = description,
                URLslug = urlSLUG,

            };

            context.BPCAT.Add(newCat);
            context.SaveChanges();

            return RedirectToAction("NewPost");
        }

        [HttpGet]
        public IActionResult NewTag()
        {
            return View("AddTag");
        }

        [HttpPost]
        public IActionResult CreateTag(string name, string description, string urlSLUG)
        {
            Tag newTag = new Tag()
            {
                Name = name,
                Description = description,
                URLslug = urlSLUG,
            };

            context.BPTag.Add(newTag);
            context.SaveChanges();

            return RedirectToAction("NewPost");
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
