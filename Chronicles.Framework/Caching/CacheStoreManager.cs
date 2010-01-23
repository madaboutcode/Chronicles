using System;
using System.IO;
using System.Web;

namespace Chronicles.Framework.Caching
{
    public class CacheStoreManager:IDisposable
    {
        private readonly ICacheProvider provider;

        public CacheStoreManager(ICacheProvider provider)
        {
            this.provider = provider;
            this.provider.Initialize();
        }

        private CacheStoreManager()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if( HttpContext.Current != null)
            {
                basePath = HttpContext.Current.Request.PhysicalApplicationPath;
            }

            if(basePath == null)
                return;

            basePath = string.Format("{0}\\PageCache",basePath.TrimEnd(new[] {'\\', '/'}));

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            provider = new FileSystemCacheProvider(basePath);
            provider.Initialize();
        }

        public void Add<T>(string key, T objGraph)
        {
            provider.Save(key, objGraph);
        }

        public TResult Fetch<TResult>(string key) where TResult:class
        {
            return provider.Fetch<TResult>(key);
        }

        public void Dispose()
        {
            provider.Dispose();
        }

        #region Singleton Implementation

        static readonly CacheStoreManager instance = new CacheStoreManager();
        public static CacheStoreManager Instance
        {
            get
            {
                return instance;
            }
        }

        // As per jon skeet. If I don't do this horrible things will happen! :)
        // http://www.yoda.arachsys.com/csharp/singleton.html
        static CacheStoreManager()
        {

        } 

        #endregion Singleton Implementation

        public void DeleteAll()
        {
            provider.DeleteAll();
        }
    }
}
