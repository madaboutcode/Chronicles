using System;
using Chronicles.Entities;

namespace Chronicles.Web.ViewModels
{
    public class PostDetails
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Id { get; set; }
        public DateTime PublishedDate { get; set; }
        public Tag[] Tags { get; set; }
        public CommentDetails[] Comments { get; set; }
        public CommentDetails EditedComment { get; set; }
    }
}
