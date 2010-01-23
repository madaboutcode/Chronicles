using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chronicles.Framework.Caching
{
    public abstract class CacheProviderBase : ICacheProvider
    {
        private IFormatter formatter = new BinaryFormatter();

        #region ICacheProvider members
        public void Initialize()
        {
        }

        public abstract void Save<T>(string key, T objectToCache);
        public abstract TResult Fetch<TResult>(string key) where TResult : class;
        public abstract void Delete(string key);
        public abstract void DeleteAll();

        public void Dispose()
        {
        }
        #endregion ICacheProvider members

        #region Helper methods for providers (serialize, deserialize etc..)

        public void Serialize(Stream serializationStream, object obj)
        {
            formatter.Serialize(serializationStream, obj);
        }

        public object Deserialize(Stream serializationStream)
        {
            return formatter.Deserialize(serializationStream);
        }

        #endregion Helper methods for providers (serialize, deserialize etc..)
    }
}