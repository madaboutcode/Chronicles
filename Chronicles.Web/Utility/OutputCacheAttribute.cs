using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Chronicles.Framework.Caching;

namespace Chronicles.Web.Utility
{
    public class ChroniclesOutputCacheAttribute:ActionFilterAttribute
    {
        private bool compress = true;

        public bool Compress
        {
            get { return this.compress; }
            set { this.compress = value; }
        }

        private readonly OutputCacheManager cacheManager; 
        public ChroniclesOutputCacheAttribute()
        {
            cacheManager = new OutputCacheManager();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            cacheManager.StartCaching(filterContext, Compress);
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            cacheManager.StopCaching(filterContext,Compress);
        }
    }
}
