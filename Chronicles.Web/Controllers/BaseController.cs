using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Chronicles.Web.Utility;

namespace Chronicles.Web.Controllers
{
    [ElmahHandleError]
    public abstract partial class BaseController : Controller
    {
        ILog logger;
        public BaseController(ILog logger)
        {
            this.logger = logger;
        }
    }
}
