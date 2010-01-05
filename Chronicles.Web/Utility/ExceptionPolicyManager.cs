using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronicles.Framework;

namespace Chronicles.Web.Utility
{
    public class ExceptionPolicyManager
    {
        public void ProcessException(Exception ex)
        {
            if(ex == null)
                return;

            Type exType = ex.GetType();

            if (typeof(RequestedResourceNotFoundException).IsAssignableFrom(exType))
                throw new HttpException(404, ex.Message, ex);
        }
    }
}
