using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;

namespace PGAlineupBuilder.Models
{
    public class Category
    {
        public virtual int ID { get; set;  }

        public virtual string Name { get; set; }

        public virtual string URLslug { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<BlogPost> BPosts { get; set; }

        public Category()
        {
            List<BlogPost> BPosts = new List<BlogPost>();
        }

        public Category(IEnumerable<BlogPost> posts)
        {
            List<BlogPost> BPosts = new List<BlogPost>();

            foreach(BlogPost post in posts)
            {
                BPosts.Add(post);
            }

        }
    }
}
