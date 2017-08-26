using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Mvc;


namespace PGAlineupBuilder.Models
{
    public class BlogPost
    {
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }
       
        public virtual string Content { get; set; }

        public virtual DateTime PublishedDate { get; set; }

        public virtual DateTime Modified { get; set; }

        public virtual string Meta { get; set; }

        public virtual string URLslug { get; set; }

        public virtual Category Category { get; set; }

        
        
        public virtual Tag Tag { get; set; }

        

        public virtual IList<BlogPostTag> BlogPostTags { get; set; }

        public virtual IList<Comment> Comments { get; set; }

        public BlogPost()
        {
            List<BlogPostTag> BlogPostTags = new List<BlogPostTag>();

            List<Comment> Comments = new List<Comment>();
            

        }

        
         

    }
}
