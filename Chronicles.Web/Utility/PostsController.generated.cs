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
    public partial class PostsController {
        protected PostsController(Dummy d) { }

        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = (IT4MVCActionResult)result;
            return RedirectToRoute(callInfo.RouteValues);
        }

        [NonAction]
        public System.Web.Mvc.ActionResult ViewPost() {
            return new T4MVC_ActionResult(Area, Name, Actions.ViewPost);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult ViewPostsByTag() {
            return new T4MVC_ActionResult(Area, Name, Actions.ViewPostsByTag);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult AddComment() {
            return new T4MVC_ActionResult(Area, Name, Actions.AddComment);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult DeleteComment() {
            return new T4MVC_ActionResult(Area, Name, Actions.DeleteComment);
        }
        [NonAction]
        public System.Web.Mvc.ActionResult UndeleteComment() {
            return new T4MVC_ActionResult(Area, Name, Actions.UndeleteComment);
        }

        public readonly string Area = "";
        public readonly string Name = "Posts";

        static readonly ActionNames s_actions = new ActionNames();
        public ActionNames Actions { get { return s_actions; } }
        public class ActionNames {
            public readonly string ViewPost = "ViewPost";
            public readonly string ViewPostsByTag = "ViewPostsByTag";
            public readonly string AddComment = "AddComment";
            public readonly string DeleteComment = "DeleteComment";
            public readonly string UndeleteComment = "UndeleteComment";
            public readonly string RssFeed = "RssFeed";
        }


        static readonly ViewNames s_views = new ViewNames();
        public ViewNames Views { get { return s_views; } }
        public class ViewNames {
            public readonly string RssFeed = "~/Views/Posts/RssFeed.aspx";
            public readonly string ViewPost = "~/Views/Posts/ViewPost.aspx";
            public readonly string ViewPostsByTag = "~/Views/Posts/ViewPostsByTag.aspx";
        }
    }

    [CompilerGenerated]
    public class T4MVC_PostsController: Chronicles.Web.Controllers.PostsController {
        public T4MVC_PostsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ViewPost(int year, int month, int day, int id, string title) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.ViewPost);
            callInfo.RouteValues.Add("year", year);
            callInfo.RouteValues.Add("month", month);
            callInfo.RouteValues.Add("day", day);
            callInfo.RouteValues.Add("id", id);
            callInfo.RouteValues.Add("title", title);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ViewPostsByTag(string tagname, int pageNumber) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.ViewPostsByTag);
            callInfo.RouteValues.Add("tagname", tagname);
            callInfo.RouteValues.Add("pageNumber", pageNumber);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddComment(Chronicles.Web.ViewModels.CommentDetails commentDetails) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.AddComment);
            callInfo.RouteValues.Add("commentDetails", commentDetails);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteComment(int commentId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.DeleteComment);
            callInfo.RouteValues.Add("commentId", commentId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UndeleteComment(int commentId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.UndeleteComment);
            callInfo.RouteValues.Add("commentId", commentId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RssFeed() {
            var callInfo = new T4MVC_ActionResult(Area, Name, Actions.RssFeed);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
