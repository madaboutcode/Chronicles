using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate;
using Chronicles.Framework;
using Chronicles.Entities;

namespace Chronicles.DataAccess.Context
{
    public class DbContext
    {
        public ISession Session { get; set; }

        DataProviderSessionFactory sessionFactory;
        public DbContext(DataProviderSessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            Session = sessionFactory.GetSession();
        }

        #region Entity Aliases
        public IQueryable<Post> Posts
        {
            get
            {
                return Session.Query<Post>();
            }
        }

        public IQueryable<Tag> Tags
        {
            get
            {
                return Session.Query<Tag>();
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                return Session.Query<Comment>();
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                return Session.Query<User>();
            }
        }
        #endregion Entity Aliases
    }
}
