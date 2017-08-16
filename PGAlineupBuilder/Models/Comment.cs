using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class Comment
    {
        public virtual int ID { get; set; }

        public virtual string Content { get; set; }

        public virtual DateTime CommentDate { get; set; }

        public virtual DateTime Modified { get; set; }

        public virtual string Meta { get; set; }

        public virtual string URLslug { get; set; }


    }
}
