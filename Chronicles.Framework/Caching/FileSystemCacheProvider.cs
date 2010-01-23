using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Chronicles.Framework.Caching
{
    public class FileSystemCacheProvider : CacheProviderBase
    {
        private static readonly Regex FilePathRegex = new Regex(@"[\/\\:\*\?<>|]", RegexOptions.Compiled);

        private readonly string storageLocation;

        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        public FileSystemCacheProvider(string storageLocation)
        {
            this.storageLocation = storageLocation.TrimEnd(new[] { '/', '\\' });
        }

        private static string MapKey(string key)
        {
            string cleanedUpKey = FilePathRegex.Replace(key, "_");

            return cleanedUpKey.Length < 250 ? cleanedUpKey : ComputeHash(cleanedUpKey);
        }

        private static string ComputeHash(string key)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(key)));
        }

        private string MapKeyToFile(string key)
        {
            string actualKey = MapKey(key);

            return string.Format("{0}\\{1}.txt", storageLocation, actualKey);
        }

        public override void Save<T>(string key, T objectToCache)
        {
            string filePath = MapKeyToFile(key);

            //I know.. taking the risk.. using write lock here will be too costly! 
            cacheLock.EnterReadLock();
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    Serialize(stream, objectToCache);
                    stream.Close();
                }
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public override TResult Fetch<TResult>(string key)
        {
            string filePath = MapKeyToFile(key);
            object obj;

            cacheLock.EnterReadLock();
            try
            {
                if (!File.Exists(filePath))
                    return null;

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    obj = Deserialize(stream);
                    stream.Close();
                }
            }
            finally
            {
                cacheLock.ExitReadLock();
            }

            return obj as TResult;
        }

        public override void Delete(string key)
        {
            string filePath = MapKeyToFile(key);

            cacheLock.EnterWriteLock();

            try
            {
                if (!File.Exists(filePath))
                    return;

                File.Delete(filePath);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public override void DeleteAll()
        {
            cacheLock.EnterWriteLock();

            try
            {
                foreach (string file in Directory.GetFiles(storageLocation))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException) { /* eat it! */ }
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}