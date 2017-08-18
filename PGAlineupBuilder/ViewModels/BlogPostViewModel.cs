using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PGAlineupBuilder.ViewModels
{
    public class BlogPostViewModel
    {
        public BlogPost Post { get; set; }

        public Category Category { get; set; }

        public IList<Tag> Tagz { get; set;}

        public BlogPostViewModel()
        {
            List<Tag> Tagz = new List<Tag>();
        }


    

    }
}
