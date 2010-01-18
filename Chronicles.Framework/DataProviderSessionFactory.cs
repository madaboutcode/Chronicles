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
        private IDataSessionFactoryProvider sessionFactoryProvider;

        public DataProviderSessionFactory(IDataSessionFactoryProvider sessionFactoryProvider)
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
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
            return sessionFactoryProvider
                .GetSessionFactory()
                .OpenSession();
        }
    }
}
