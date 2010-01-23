using System;

namespace Chronicles.Framework.Caching
{
    public interface ICacheProvider : IDisposable
    {
        void Initialize();
        void Save<T>(string key, T objectToCache);
        TResult Fetch<TResult>(string key) where TResult:class;
        void Delete(string key);
        void DeleteAll();
    }
}