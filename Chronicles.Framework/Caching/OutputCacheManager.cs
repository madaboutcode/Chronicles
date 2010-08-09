using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Chronicles.Framework.Caching
{
    public class OutputCacheManager
    {
        private string currentCacheKey = null;

        static readonly MethodInfo SwitchWriterMethod = typeof(HttpResponse).GetMethod("SwitchWriter",
                                                                            BindingFlags.Instance |
                                                                            BindingFlags.NonPublic);

        private TextWriter cacheWriter = null;
        private TextWriter httpWriterInUse = null;

        private string GetCacheKey(ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            ControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;
            StringBuilder cacheKeyBuilder = new StringBuilder();

            if (HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
                cacheKeyBuilder.Append("[Auth]");

            cacheKeyBuilder.AppendFormat("{0}.{1}",
                                         controllerDescriptor.ControllerType.Name,
                                         actionDescriptor.ActionName);
            cacheKeyBuilder.Append("(");
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    cacheKeyBuilder.Append('[');
                    string name = item.Key;
                    int hash = item.Value == null ? 0 : item.Value.GetHashCode();
                    cacheKeyBuilder.AppendFormat("{0}:{1}", name, hash);
                    cacheKeyBuilder.Append(']');
                }
            }
            cacheKeyBuilder.Append(")");

            return cacheKeyBuilder.ToString();
        }

        private string GetCacheKey(ActionExecutingContext context)
        {
            return GetCacheKey(context.ActionDescriptor, context.ActionParameters);
        }

        public void StartCaching(ActionExecutingContext filterContext, bool compress)
        {
            this.currentCacheKey = GetCacheKey(filterContext);

            CachedPage cachedPage = CacheStoreManager.Instance.Fetch<CachedPage>(this.currentCacheKey);

            if (cachedPage == null)
            {
                this.cacheWriter = new StringWriter();
                httpWriterInUse =
                    SwitchWriterMethod.Invoke(HttpContext.Current.Response, new object[] {this.cacheWriter}) as
                    TextWriter;
            }
            else
            {
                SendPageToClient(ref filterContext, cachedPage);
            }
        }

        public void StopCaching(ResultExecutedContext filterContext, bool compress)
        {
            if(httpWriterInUse != null)
            {
                HttpResponseBase response = filterContext.HttpContext.Response;

                string output = cacheWriter.ToString();

                if (response.ContentType == "text/html")
                    output = string.Format("{0}<!-- generated at {1} -->"
                                   , output
                                   , DateTime.Now.ToString("r"));

                byte[] contentBytes = response.ContentEncoding.GetBytes(output);
                
                CachedPage page = new CachedPage
                                      {
                                          Content = compress? Compress(contentBytes) : contentBytes
                                          , ContentType = response.ContentType
                                          , Time = DateTime.Now
                                          , IsCompressed = compress
                                      };
                SwitchWriterMethod.Invoke(HttpContext.Current.Response, new object[] {httpWriterInUse});
                SendPageToClient(ref filterContext, page);
                Save(page, compress);
            }
        }

        public static void EntityChanged<T>(T entity) where T:class 
        {
            CacheStoreManager.Instance.DeleteAll();
        }

        private void Save(CachedPage page, bool compress)
        {
            if(compress && page.IsCompressed == false)
            {
                page.Content = Compress(page.Content);
                page.IsCompressed = true;
            }
            CacheStoreManager.Instance.Add(currentCacheKey, page);
        }

        private static byte[] Compress(byte[] content)
        {
            using (MemoryStream memStream = new MemoryStream())
            using(GZipStream compressionStream = new GZipStream(memStream, CompressionMode.Compress))
            {
                compressionStream.Write(content, 0, content.Length);
                compressionStream.Close();
                return memStream.ToArray();
            }
        }

        private void SendPageToClient(ref ResultExecutedContext context, CachedPage page)
        {
            byte[] result = PreparePage(page, context.HttpContext);

            if (context.IsChildAction)
                httpWriterInUse.Write(Encoding.UTF8.GetString(result));
            else
            {
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = "text/html";
                response.Charset = Encoding.UTF8.WebName;
                response.BinaryWrite(result);
            }
        }

        private void SendPageToClient(ref ActionExecutingContext filterContext, CachedPage cachedPage)
        {
            byte[] result = PreparePage(cachedPage, filterContext.HttpContext);

            HttpResponseBase response = filterContext.HttpContext.Response;
            response.ContentType = "text/html";
            response.Charset = Encoding.UTF8.WebName;

            if (cachedPage.IsCompressed == false)
                filterContext.Result = new ContentResult { Content = Encoding.UTF8.GetString(result) };
            else
                filterContext.Result = new BinaryResult { Content = result };
        }

        private byte[] PreparePage(CachedPage page, HttpContextBase httpContext) 
        {
            byte[] result = page.Content;

            httpContext.Response.ContentType = page.ContentType;

            if (page.IsCompressed)
            {
                HttpRequestBase request = httpContext.Request;
                string acceptEncoding = request.Headers["Accept-Encoding"];

                bool compressionSupported = false;

                HttpResponseBase response = httpContext.Response;

                if (!string.IsNullOrEmpty(acceptEncoding))
                {
                    acceptEncoding = acceptEncoding.ToUpperInvariant();

                    if (acceptEncoding.IndexOf("GZIP", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        response.AppendHeader("Content-encoding", "gzip");
                        compressionSupported = true;
                    }
                }
                if(!compressionSupported)
                {
                    return Decompress(result);
                }
            }

            return result;
        }

        private static byte[] Decompress(byte[] content)
        {
            List<byte> result = new List<byte>(10000);
            byte[] buffer = new byte[10000];
            
            using (MemoryStream memStream = new MemoryStream(content))
            using (GZipStream compressionStream = new GZipStream(memStream, CompressionMode.Decompress))
            {
                int bytesRead = 0;
                while((bytesRead = compressionStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Array.Resize(ref buffer,bytesRead);
                    result.AddRange(buffer);
                    buffer=new byte[10000];
                }
            }

            return result.ToArray();
        }
    }
}