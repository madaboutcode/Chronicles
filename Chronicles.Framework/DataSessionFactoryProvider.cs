using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

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
                .BuildSessionFactory();
        }

        public ISessionFactory GetSessionFactory()
        {
            return factory;
        }
    }
}
