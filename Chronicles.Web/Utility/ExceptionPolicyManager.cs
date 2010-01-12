using System;
using System.Collections.Generic;
using System.Web;
using Chronicles.Framework;
using Elmah;

namespace Chronicles.Web.Utility
{
    public class ExceptionPolicyManager
    {
        public void ProcessException(Exception ex, HttpContext ctx)
        {
            if(ex == null)
                return;

            Type exType = ex.GetType();

            if (typeof(RequestedResourceNotFoundException).IsAssignableFrom(exType))
                ex = new HttpException(404, ex.Message, ex);

            ErrorLog.GetDefault(ctx).Log(new Error(ex, ctx));
        }
    }
}
