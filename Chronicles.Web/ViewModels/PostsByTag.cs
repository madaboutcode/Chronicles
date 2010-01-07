using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronicles.Entities;

namespace Chronicles.Web.ViewModels
{
    public class PostsByTag
    {
        public IList<PostSummary> Posts { get; set; }
        public int TotalPages { get; set; }
        public Tag Tag { get; set; }
    }
}
