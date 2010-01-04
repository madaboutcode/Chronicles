using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Context;
using NHibernate;

namespace Chronicles.DataAccess
{
    public abstract class RepositoryBase
    {
        protected RepositoryBase(DbContext ctx)
        {
            DbContext = ctx;
        }

        protected DbContext DbContext { get; private set; }

        protected ISession Session
        {
            get
            {
                return DbContext.Session;
            }
        }
    }
}
