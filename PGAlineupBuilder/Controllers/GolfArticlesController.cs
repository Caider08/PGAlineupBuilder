using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Data;
using PGAlineupBuilder.ViewModels;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace PGAlineupBuilder.Controllers
{
    public class GolfArticlesController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public GolfArticlesController(PGAlineupBuilderDbContext dbcontext)
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
            if (ModelState.IsValid)
            {

                Category BlogCategory = context.BPCAT.Single(c => c.ID == createBlog.CategoryID);

                Tag BlogTag = context.BPTag.Single(c => c.ID == createBlog.TagID);

                string descriptionMeta = createBlog.content.Take(150).ToString();

                string cleanSlug = createBlog.Name.ToLower().Replace(" ", "-");
                cleanSlug = Regex.Replace(cleanSlug, @"[^a-zA-Z0-9\/_|+ -]", "");


                BlogPost bpost = new BlogPost()
                {
                    Name = createBlog.Name,
                    Content = createBlog.content,
                    PublishedDate = DateTime.Now,
                    Meta = descriptionMeta,
                    URLslug = cleanSlug,

                    Category = BlogCategory,
                    Tag = BlogTag,

                   

                };

                if(context.BP.Where(bp => bp.Name == bpost.Name).ToList<BlogPost>().Count() > 0)
                {
                    return RedirectToAction("NewPost");
                }
                else
                {
                    context.BP.Add(bpost);
                    context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                
                
            }

            return View();
           
        }
        
        [HttpGet]
        public IActionResult GetBlogPost(int ID)
        {
            BlogPost grabbedBlog = context.BP.Include(x => x.Category).Include(x => x.Tag).FirstOrDefault(bp => bp.ID == ID);
            string PostName = grabbedBlog.Name;
            return RedirectToAction("GetGolfArticle", new { PostName = $"{PostName}" });
            //return View("BlogPost", grabbedBlog);
        }

        [HttpGet]
        public IActionResult GetGolfArticle(string PostName)
        {
            BlogPost viewArticle = context.BP.Include(a => a.Category).Include(a => a.Tag).FirstOrDefault(bp => bp.Name == PostName);
            return View("BlogPost", viewArticle);
        }

        [HttpGet]
        public IActionResult CategoryBlogs(int ID)
        {
            IList<BlogPost> grabbedBlogs = context.BP.Include(x => x.Category).Include(x => x.Tag).Where(bp => bp.Category.ID == ID).Take(10).ToList<BlogPost>();
            Category exampleCategory = context.BPCAT.FirstOrDefault(c => c.ID == ID);

            ViewBag.header = exampleCategory;
            return View("ListBlogs", grabbedBlogs);
        }

        [HttpGet]
        public IActionResult TagBlogs(int ID)
        {
            IList<BlogPost> grabbedBlogs = context.BP.Include(x => x.Category).Include(x => x.Tag).Where(bp => bp.Tag.ID == ID).Take(10).ToList<BlogPost>();
            Tag exampleTag = context.BPTag.FirstOrDefault(t => t.ID == ID);

            ViewBag.header = exampleTag;
            return View("ListBlogs", grabbedBlogs);
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

        public IActionResult AllBlogs()
        {
            IList<BlogPost> blogs = context.BP.OrderByDescending(p => p.PublishedDate).ToList<BlogPost>();

            return View("BlogView",blogs);
        }

        [HttpGet]
        public IActionResult ArticleSearch(string Aterm)
        {
            if(!string.IsNullOrWhiteSpace(Aterm))
            {
                IList<BlogPost> searchArticles = context.BP.Include(a => a.Category).Include(a => a.Tag).Where(bp => bp.Name.Contains(Aterm)).Take(10).ToList<BlogPost>();
               

                if (searchArticles.Count() > 0)
                {
                    ViewBag.header = searchArticles.FirstOrDefault();
                    return View("ListBlogs", searchArticles);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public IActionResult byTerm(string sTerm, string SearchMethod)
        {
            if(!string.IsNullOrWhiteSpace(sTerm))
            {
                IList<BlogPost> searchPosts = new List<BlogPost>();

                if(SearchMethod == "ByTitle")
                {
                     searchPosts = context.BP.Include(p => p.Category).Include(p => p.Tag).Where(b => b.Name.Contains(sTerm)).Take(5).ToList<BlogPost>();

                }
                if(SearchMethod == "ByKeyword")
                {
                    searchPosts = context.BP.Include(p => p.Category).Include(p => p.Tag).Where(b => b.Content.Contains(sTerm)).Take(5).ToList<BlogPost>();
                }
                if(searchPosts.Count() == 0)
                {
                    ViewBag.Nothing = $"Sorry, no Blogs with {sTerm} in the title were found";
                   return View("SearchBlogs");
                }
                else
                {
                    BlogPost exampleBlog = searchPosts.FirstOrDefault();
                    
                    return View("ListBlogs", searchPosts);
                }
                
            }
            else
            {
                ViewBag.BlankTerm = "Please enter a search Term";
                return View("SearchBlogs");
            }
        }

        [HttpGet]
        public IActionResult SearchBlogs()
        {
            return View();
        }
    }
}
