// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Chronicles.Web.Controllers {
    [CompilerGenerated]
    public partial class WidgetController {
        protected WidgetController(Dummy d) { }

        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return RedirectToRoute(callInfo.RouteValues);
        }


        public readonly string Area = "";
        public readonly string Name = "Widget";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string RecentPosts = "RecentPosts";
            public readonly string PerfTracker = "PerfTracker";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
            public readonly string PerfTracker = "~/Views/Widget/PerfTracker.ascx";
            public readonly string RecentPosts = "~/Views/Widget/RecentPosts.aspx";
        }
    }

    [CompilerGenerated]
    public class T4MVC_WidgetController: Chronicles.Web.Controllers.WidgetController {
        public T4MVC_WidgetController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult RecentPosts() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.RecentPosts);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PerfTracker() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.PerfTracker);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
