using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Chronicles.Web.Utility;

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
                , new { controller="Archives", action="ViewPost",title="" }
                , new { year=@"\d{4}", month=@"\d{1,2}", day=@"\d{1,2}", id=@"\d+"}
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}",                           // URL with parameters
                new { controller = "Home", action = "Index" }  // Parameter defaults
            );

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

            //Do all the bootstrapping
            Bootstrapper.Boot();

            RegisterControllerFactory();
        }
    }
}