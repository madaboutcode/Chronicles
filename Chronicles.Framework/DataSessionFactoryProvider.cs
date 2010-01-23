using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Chronicles.Framework.Caching;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Event;

namespace Chronicles.Framework
{
    public class DataSessionFactoryProvider : IDataSessionFactoryProvider
    {
        const string EntityAssembly = "Chronicles.Entities";

        AppConfiguration config;
        private readonly ISessionFactory factory;

        public DataSessionFactoryProvider(AppConfiguration configuration)
        {
            config = configuration;

            Assembly entityAssembly = Assembly.Load(EntityAssembly);

            factory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005.ConnectionString(config.ConnectionString))
                .Mappings(m => m.FluentMappings
                                   .AddFromAssembly(entityAssembly)
                )
                .ExposeConfiguration(c => c.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[] {new NHibernateEventListener()})
                .ExposeConfiguration(c => c.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[] { new NHibernateEventListener() })
                .BuildSessionFactory();
        }

        public ISessionFactory GetSessionFactory()
        {
            return factory;
        }
    }
}
