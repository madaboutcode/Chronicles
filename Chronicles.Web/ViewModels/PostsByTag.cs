using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronicles.Web.ViewModels
{
    public class PostsByTag
    {
        public PostSummary Posts { get; set; }
        public int TotalPages { get; set; }
    }
}
