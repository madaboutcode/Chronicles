using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Chronicles.Web.Utility;
using Chronicles.Web.Controllers;

namespace Chronicles.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            // /post/2009/12/26/12345/title
            routes.MapRoute(
                "posts"
                , "post/{year}/{month}/{day}/{id}/{title}"
                , new { controller = MVC.Posts.Name, action = MVC.Posts.Actions.ViewPost, title = "" }
                , new { year = @"\d{4}", month = @"\d{1,2}", day = @"\d{1,2}", id = @"\d+" }
            );

            // /post/tagged/programming/page
            routes.MapRoute(
                "postbytags"
                , "post/tagged/{tagname}/{pagenumber}"
                , new { controller = MVC.Posts.Name, action = MVC.Posts.Actions.ViewPostsByTag, pagenumber = 1 }
                , new { pagenumber = @"\d+" }
            );

            //  /subscribe
            routes.MapRoute(
                "subscribe"
                , "subscribe"
                , new {controller = MVC.Posts.Name, action = MVC.Posts.Actions.RssFeed});

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Home", action = "Index" }  // Parameter defaults,
                , new { controller = @"[a-zA-Z]*", action = @"[a-zA-Z]*" }
            );
        }

        public void RegisterModelBinders(ModelBinderDictionary binders) // Add this whole method
        {
            //binders.DefaultBinder = new DataAnnotationsModelBinder();
        }

        public static void RegisterControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(new DIControllerFactory());
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);

            RegisterModelBinders(ModelBinders.Binders);

            //Do all the bootstrapping
            Bootstrapper.Boot();

            RegisterControllerFactory();

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        protected void Application_Error()
        {
            // At this point we have information about the error
            HttpContext ctx = HttpContext.Current;

            Exception exception = ctx.Server.GetLastError();

            if (exception != null)
            {
                new ExceptionPolicyManager().ProcessException(exception,ctx);
            }
        }
    }
}