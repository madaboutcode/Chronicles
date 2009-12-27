using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace Chronicles.Framework
{
    public class DataProviderSessionFactory
    {
        const string EntityAssembly = "Chronicles.Entities";

        AppConfiguration config;

        public DataProviderSessionFactory(AppConfiguration config)
        {
            this.config = config;
        }

        private ISession session;
        public ISession GetSession()
        {
            if (session == null)
                    session = CreateSession();

             return session;
        }

        private ISession CreateSession()
        {
            Assembly entityAssembly = Assembly.Load(EntityAssembly);

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(config.ConnectionString))
                .Mappings(m => m.FluentMappings
                                .AddFromAssembly(entityAssembly)
                 )
                .BuildSessionFactory()
                .OpenSession();
        }
    }
}
