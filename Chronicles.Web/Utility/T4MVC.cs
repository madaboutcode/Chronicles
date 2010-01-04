﻿// <auto-generated />
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

[CompilerGenerated]
public static class MVC {
    public static Chronicles.Web.Controllers.CommentController Comment = new Chronicles.Web.Controllers.T4MVC_CommentController();
    public static Chronicles.Web.Controllers.HomeController Home = new Chronicles.Web.Controllers.T4MVC_HomeController();
    public static Chronicles.Web.Controllers.PostsController Posts = new Chronicles.Web.Controllers.T4MVC_PostsController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC {
}

namespace System.Web.Mvc {
    [CompilerGenerated]
    public static class T4Extensions {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result) {
            return htmlHelper.RouteLink(linkText, result.GetRouteValueDictionary());
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes) {
            return ActionLink(htmlHelper, linkText, result, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes) {
            return htmlHelper.RouteLink(linkText, result.GetRouteValueDictionary(), htmlAttributes);
        }

        public static MvcForm BeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod) {
            return htmlHelper.BeginForm(result, formMethod, null);
        }

        public static MvcForm BeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod, object htmlAttributes) {
            return BeginForm(htmlHelper, result, formMethod, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcForm BeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod, IDictionary<string, object> htmlAttributes) {
            var callInfo = (IT4MVCActionResult)result;
            return htmlHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValues, formMethod, htmlAttributes);
        }

        public static void RenderAction(this HtmlHelper htmlHelper, ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            htmlHelper.RenderAction(callInfo.Action, callInfo.Controller, callInfo.RouteValues);
        }

        public static MvcHtmlString Action(this HtmlHelper htmlHelper, ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return htmlHelper.Action(callInfo.Action, callInfo.Controller, callInfo.RouteValues);
        }
        public static string Action(this UrlHelper urlHelper, ActionResult result) {
            return urlHelper.RouteUrl(result.GetRouteValueDictionary());
        }

        public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions) {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions);
        }

        public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions, object htmlAttributes) {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes) {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions, htmlAttributes);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result) {
            return routes.MapRoute(name, url, result, (ActionResult)null);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, object defaults) {
            // Start by adding the default values from the anonymous object (if any)
            var routeValues = new RouteValueDictionary(defaults);

            // Then add the Controller/Action names and the parameters from the call
            foreach (var pair in result.GetRouteValueDictionary()) {
                routeValues.Add(pair.Key, pair.Value);
            }

            // Create and add the route
            var route = new Route(url, routeValues, new MvcRouteHandler());
            routes.Add(name, route);
            return route;
        }

        public static RouteValueDictionary GetRouteValueDictionary(this ActionResult result) {
            return ((IT4MVCActionResult)result).RouteValues;
        }

        public static void InitMVCT4Result(this IT4MVCActionResult result, string area, string controller, string action) {
            result.Controller = controller;
            result.Action = action;
            result.RouteValues = new RouteValueDictionary();
            result.RouteValues.Add("Area", area ?? "");
            result.RouteValues.Add("Controller", controller);
            result.RouteValues.Add("Action", action);
        }

        public static bool FileExists(string virtualPath) {
            if (!HostingEnvironment.IsHosted) return false;
            string filePath = HostingEnvironment.MapPath(virtualPath);
            return System.IO.File.Exists(filePath);
        }
    }
}

[CompilerGenerated]
public interface IT4MVCActionResult {
    string Action { get; set; }
    string Controller { get; set; }
    RouteValueDictionary RouteValues { get; set; }
}

[CompilerGenerated]
public class T4MVC_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult {
    public T4MVC_ActionResult(string area, string controller, string action): base()  {
        this.InitMVCT4Result(area, controller, action);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public RouteValueDictionary RouteValues { get; set; }
}



namespace Links {
    [CompilerGenerated]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string chronicles_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/chronicles.min.js") ? Url("chronicles.min.js") : Url("chronicles.js");
        public static readonly string jquery_1_3_2_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.3.2-vsdoc.min.js") ? Url("jquery-1.3.2-vsdoc.min.js") : Url("jquery-1.3.2-vsdoc.js");
        public static readonly string jquery_1_3_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.3.2.min.js") ? Url("jquery-1.3.2.min.js") : Url("jquery-1.3.2.js");
        public static readonly string jquery_1_3_2_min_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.3.2.min-vsdoc.min.js") ? Url("jquery-1.3.2.min-vsdoc.min.js") : Url("jquery-1.3.2.min-vsdoc.js");
        public static readonly string jquery_1_3_2_min_js = Url("jquery-1.3.2.min.js");
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate-vsdoc.min.js") ? Url("jquery.validate-vsdoc.min.js") : Url("jquery.validate-vsdoc.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_min_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min-vsdoc.min.js") ? Url("jquery.validate.min-vsdoc.min.js") : Url("jquery.validate.min-vsdoc.js");
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");
        public static readonly string MicrosoftAjax_debug_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftAjax.debug.min.js") ? Url("MicrosoftAjax.debug.min.js") : Url("MicrosoftAjax.debug.js");
        public static readonly string MicrosoftAjax_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftAjax.min.js") ? Url("MicrosoftAjax.min.js") : Url("MicrosoftAjax.js");
        public static readonly string MicrosoftMvcAjax_debug_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftMvcAjax.debug.min.js") ? Url("MicrosoftMvcAjax.debug.min.js") : Url("MicrosoftMvcAjax.debug.js");
        public static readonly string MicrosoftMvcAjax_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftMvcAjax.min.js") ? Url("MicrosoftMvcAjax.min.js") : Url("MicrosoftMvcAjax.js");
        public static readonly string MicrosoftMvcValidation_debug_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftMvcValidation.debug.min.js") ? Url("MicrosoftMvcValidation.debug.min.js") : Url("MicrosoftMvcValidation.debug.js");
        public static readonly string MicrosoftMvcValidation_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/MicrosoftMvcValidation.min.js") ? Url("MicrosoftMvcValidation.min.js") : Url("MicrosoftMvcValidation.js");
    }

    [CompilerGenerated]
    public static class css {
        private const string URLPATH = "~/css";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string master_css = Url("master.css");
    }

    [CompilerGenerated]
    public static class img {
        private const string URLPATH = "~/img";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string blogtitle_bg_jpg = Url("blogtitle_bg.jpg");
        public static readonly string dot_png = Url("dot.png");
        public static readonly string dotline_png = Url("dotline.png");
        public static readonly string form_ico_gif = Url("form-ico.gif");
        public static readonly string logo_jpg = Url("logo.jpg");
        public static readonly string quote_jpg = Url("quote.jpg");
        public static readonly string rss_jpg = Url("rss.jpg");
        public static readonly string tag_png = Url("tag.png");
    }

}

static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    public static string ProcessVirtualPath(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }
    
    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}




namespace T4MVC {
    [CompilerGenerated]
    public class Dummy {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

#endregion T4MVC
#pragma warning restore 1591


