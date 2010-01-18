using System.Web.Mvc;
using System.Web.UI;

namespace Chronicles.Web.Utility
{
    public static class ViewHelpers
    {
        public static bool IsAdmin(this Control control)
        {
            //TODO: the obvious! :)
            if (control.Page.User == null
                || control.Page.User.Identity == null
                || control.Page.User.Identity.IsAuthenticated == false)
                return false;

            return string.Compare(control.Page.User.Identity.Name,"ajeesh") == 0;
        }
    }
}
