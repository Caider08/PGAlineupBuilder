using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using System.Net;
using PGAlineupBuilder.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authorization;

namespace PGAlineupBuilder.Controllers
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

    [Authorize(Roles ="PGAguru")]
    public class ManageGolfArticlesController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public ManageGolfArticlesController(PGAlineupBuilderDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<BlogPost> currentArticles = context.BP.Include(x => x.Category).Include(x => x.Tag).ToList<BlogPost>();

            return View(currentArticles);
        }

        public IActionResult EditArticle(int ID)
        {
            BlogPost article = context.BP.Include(c => c.Category).Include(c => c.Tag).Single(p => p.ID == ID);
            IList<Tag> tagz = context.BPTag.ToList<Tag>();
            IList<Category> catz = context.BPCAT.ToList<Category>();

            NewBlogPostViewModel model = new NewBlogPostViewModel(tagz, catz)
            {
                Name = article.Name,
                content = article.Content,
                meta = article.Meta,
                urlSlug = article.URLslug,
                id = ID,

            };

            ViewBag.Article = article;
            return View(model);
        }

        [HttpPost]
        public IActionResult ArticleChanges(NewBlogPostViewModel model)
        {
            BlogPost articleToAdjust = context.BP.Include(x => x.Category).Include(x => x.Tag).Single(p => p.ID == model.id);

            Category BlogCategory = context.BPCAT.Single(c => c.ID == model.CategoryID);

            Tag BlogTag = context.BPTag.Single(c => c.ID == model.TagID);

            //Begin tracking changes to our Article
            context.BP.Update(articleToAdjust);

            articleToAdjust.Name = model.Name;
            articleToAdjust.Content = model.content;
            articleToAdjust.Meta = model.meta;
            articleToAdjust.URLslug = model.urlSlug;
            articleToAdjust.Tag = BlogTag;
            articleToAdjust.Category = BlogCategory;
            
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
           // return RedirectToAction("GetGolfArticle", "GolfArticles", new { PostName = $"{articleToAdjust.Name}" });
        }

        public ContentResult Posts(JqGridInViewModel JqParams)
        {
            IList<BlogPost> bPosts = new List<BlogPost>();

            if (JqParams.sord == "desc")
            {
                bPosts = context.BP.Include(p => p.Category).Include(p => p.Tag).OrderByDescending(p => p.PublishedDate).Take(JqParams.rows).ToList<BlogPost>();
            }
            else
            {
                 bPosts = context.BP.Include(p => p.Category).Include(p => p.Tag).OrderBy(p => p.PublishedDate).Take(JqParams.rows).ToList<BlogPost>();
            }

            var totalPosts = bPosts.Count();

            return Content(JsonConvert.SerializeObject(new
            {
                page = JqParams.page,
                records = totalPosts,
                rows = bPosts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / JqParams.rows)
            }, new CustomDateTimeConverter()), "application/json");

            
        }
    }
}
