using System;
using System.Collections.Generic;

namespace Chronicles.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Hash { get; set; }
        public virtual string Salt { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Email { get; set; }
        public virtual UserRole Role { get; set; }
        public virtual string WebSite { get; set; }
        public virtual IList<Comment> Comments { get; set; }

        public User()
        {
            Comments = new List<Comment>();
        }
    }

    public enum UserRole
    {
        Visitor = 0,
        Admin = 1,
        Reviewer = 2
    }
}