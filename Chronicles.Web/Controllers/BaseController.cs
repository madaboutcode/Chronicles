using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Chronicles.Web.Controllers
{
    public abstract partial class BaseController : Controller
    {
        ILog logger;
        public BaseController(ILog logger)
        {
            this.logger = logger;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            
            //base.OnException(filterContext);
        }
    }
}
