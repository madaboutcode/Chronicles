using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Entities
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual DateTime ScheduledDate { get; set; }
        public virtual int UserId { get; set; }
        public virtual bool Approved { get; set; }
        public virtual IList<Tag> Tags { get; protected set; }
        public virtual int CommentCount { get; set; }
        public virtual IList<Comment> Comments { get; set; }

        public virtual void AddTag(Tag tag)
        {
            if(Tags == null)
                Tags = new List<Tag>();

            Tag existingTag = (Tags.Where(t => t.TagName == tag.TagName)).FirstOrDefault();

            if (existingTag == null)
            {
                Tags.Add(tag);
                tag.Posts.Add(this);
            }
        }
    }
}