using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronicles.Entities;

namespace Chronicles.Web.ViewModels
{
    public class PostSummary
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public Tag[] Tags { get; set; }
        public int CommentCount { get; set; }
    }
}
