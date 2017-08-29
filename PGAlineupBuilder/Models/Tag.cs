using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class Tag
    {
        public virtual int ID { get; set;  }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string URLslug { get; set; }

        [JsonIgnore]
        public virtual IList<BlogPostTag> BlogPostTags { get; set; }

        public Tag()
        {
            List<BlogPostTag> BlogPostTags = new List<BlogPostTag>();
        }

    }
}
