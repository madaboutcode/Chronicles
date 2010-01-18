using NHibernate;

namespace Chronicles.Framework
{
    public interface IDataSessionFactoryProvider
    {
        ISessionFactory GetSessionFactory();
    }
}