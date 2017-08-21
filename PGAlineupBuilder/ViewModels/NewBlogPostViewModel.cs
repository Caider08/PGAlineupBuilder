using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PGAlineupBuilder.ViewModels
{
    public class NewBlogPostViewModel
    {
        [Required(ErrorMessage ="Please Title your Post")]
        [Display(Name="Title of Post")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plesae input the Body of your Post")]
        [Display(Name = "Post Content")]
        public string content { get; set; }

        [Display(Name="Enter meta description")]
        public string meta { get; set; }

        [Display(Name="Enter urlSlug")]
        public string urlSlug { get; set; }

        [Display(Name="Select a Category for your post from the dropdown or follow link to create a new one.")]
        public int CategoryID { get; set; }

       // [Display(Name="Enter a Category for your Post(or Select from the dropdown Below)")]
       // public string categoryName { get; set; }

       // [Display(Name="Describe the Category")]
       // public string categoryDesc { get; set; }

        [Display(Name="Select a Tag for your post from the dropdown or follow link to create a new one.")]
        public int TagID { get; set; }
       // [Display(Name="Name a tag to further categorize your Post(or Select from the dropdown Below")]
       // public string tagName { get; set; }

        //[Display(Name="Describe your chosen Tag")]
       // public string tagDesc { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> Tags { get; set; }

        public NewBlogPostViewModel()
        {
        }

        public NewBlogPostViewModel(IEnumerable<BlogPostTag> tagz, IEnumerable<Category> catz)
        {
            Categories = new List<SelectListItem>();
            Tags = new List<SelectListItem>();

            foreach(BlogPostTag tag in tagz)
            {
                Tags.Add(new SelectListItem
                {
                    Value = ((int)tag.TagID).ToString(),
                    Text = tag.Tag.Name,
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
