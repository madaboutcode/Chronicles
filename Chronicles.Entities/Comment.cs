using System;
using System.Linq;
using System.Text;

namespace Chronicles.Entities
{
    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool Approved { get; set; }
        public virtual int Deleted { get; set; }

        public virtual void AddUser(User user)
        {
            User = user;
            user.Comments.Add(this);
        }
    }
}