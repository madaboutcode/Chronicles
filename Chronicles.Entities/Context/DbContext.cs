using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate;
using Chronicles.Framework;

namespace Chronicles.Entities.Context
{
    public class DbContext : NHibernateContext
    {
        DataProviderSessionFactory sessionFactory;
        public DbContext(DataProviderSessionFactory sessionFactory)
            : base(sessionFactory.GetSession())
        {
            this.sessionFactory = sessionFactory;
        }

        #region Entity Aliases
        public IOrderedQueryable<Post> Posts
        {
            get
            {
                return Session.Linq<Post>();
            }
        }

        public IOrderedQueryable<Tag> Tags
        {
            get
            {
                return Session.Linq<Tag>();
            }
        }
        #endregion Entity Aliases
    }
}
