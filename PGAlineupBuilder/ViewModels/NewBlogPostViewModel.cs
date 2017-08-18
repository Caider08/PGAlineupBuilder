using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PGAlineupBuilder.ViewModels
{
    public class NewBlogPostViewModel
    {
        public string Name { get; set; }
        
        public string content { get; set; }

        public string meta { get; set; }

        public string urlSlug { get; set; }

        public string categoryName { get; set; }

        public string categoryDesc { get; set; }

        public string tagName { get; set; }

        public string tagDesc { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> Tags { get; set; }

        public NewBlogPostViewModel()
        {
        }

        public NewBlogPostViewModel(IEnumerable<Tag> tagz, IEnumerable<Category> catz)
        {
            Categories = new List<SelectListItem>();
            Tags = new List<SelectListItem>();

            foreach(Tag tag in tagz)
            {
                Tags.Add(new SelectListItem
                {
                    Value = ((int)tag.ID).ToString(),
                    Text = tag.Name
                });
            }

            foreach(Category cat in catz)
            {
                Categories.Add(new SelectListItem
                {
                    Value = ((int)cat.ID).ToString(),
                    Text = cat.Name
                });
            }
        }





    }
}
