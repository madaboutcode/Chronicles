using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Entities
{
    public class Tag
    {
        public virtual string TagName { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual IList<Post> Posts { get; protected set; }
        public virtual string NormalizedTagName { get; set; } 

        public Tag()
        {
            Posts = new List<Post>();
        }
    }
}
