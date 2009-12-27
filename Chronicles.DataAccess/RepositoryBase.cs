using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.Entities.Context;

namespace Chronicles.DataAccess
{
    public abstract class RepositoryBase
    {
        public RepositoryBase(DbContext ctx)
        {
            dbContext = ctx;
        }

        private DbContext dbContext;
        protected DbContext DbContext
        {
            get
            {
                return dbContext;
            }
        }
    }
}
