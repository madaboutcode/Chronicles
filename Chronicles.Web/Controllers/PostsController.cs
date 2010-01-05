using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronicles.Services;
using Chronicles.Entities;
using AutoMapper;
using Chronicles.Web.ViewModels;
using Chronicles.Framework;
using Chronicles.Web.Utility;

namespace Chronicles.Web.Controllers
{
    public partial class PostsController : BaseController
    {
        PostServices postServices;
        CommentServices commentService;

        public PostsController(PostServices postServices, CommentServices commentService,AppConfiguration config)
        {
            this.postServices = postServices;
            this.commentService = commentService;
        }

        public virtual ActionResult ViewPost(int year, int month, int day, int id, string title)
        {
            Post p = postServices.GetPostById(id);

            if (p == null)
                throw new RequestedResourceNotFoundException("Sorry, I don't have that post. Please check the url again");

            PostDetails details = Mapper.Map<Post, PostDetails>(p);

            return View(details);
        }


        public virtual ActionResult ViewPostsByTag(string tagname, int pageNumber)
        {
            int totalPages = 0;
            //TODO: build pagination
            IList<Post> posts = postServices.GetPostsByTag(tagname, 25, 1, out totalPages);

            //TODO: show not found page if there are no records
            return View(Mapper.Map<IList<Post>, PostSummary[]>(posts));
        }

        protected virtual ActionResult ViewPost(int id, CommentDetails commentDetails)
        {
            Post p = postServices.GetPostById(id);

            PostDetails details = Mapper.Map<Post, PostDetails>(p);

            details.EditedComment = commentDetails;

            return View(MVC.Posts.Views.ViewPost, details);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddComment(CommentDetails commentDetails)
        {
            if (ModelState.IsValid)
            {
                Comment comment = GetComment(commentDetails);

                commentService.AddComment(comment, commentDetails.PostId);

                return Redirect(Url.RouteUrl(comment.Post.GetActionUrl().GetRouteValueDictionary()) + "#comment-"+comment.Id);
            }
            else
                return ViewPost(commentDetails.PostId, commentDetails);
        }

        private Comment GetComment(CommentDetails commentDetails)
        {
            Comment comment = new Comment { Text = commentDetails.Text };
            User user = new User { Email = commentDetails.UserEmail, Name = commentDetails.UserName, WebSite = commentDetails.UserWebSite };
            comment.User = user;

            return comment;
        }
    }
}
