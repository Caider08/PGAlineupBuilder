using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class BlogPostTag
    {
        public int BlogPostID {get; set;}

        public BlogPost BlogPost { get; set; }

        public int TagID { get; set; }

        public Tag Tag { get; set; }


    }
}
