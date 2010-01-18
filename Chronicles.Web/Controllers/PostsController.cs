using System;
using System.Collections.Generic;
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
        private readonly PostServices postServices;
        private readonly CommentServices commentService;
        private readonly TagServices tagService;
        private readonly AppConfiguration config;

        public PostsController(PostServices postServices,TagServices tagService ,CommentServices commentService,AppConfiguration config)
        {
            this.postServices = postServices;
            this.commentService = commentService;
            this.tagService = tagService;
            this.config = config;
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

            Tag tag = tagService.GetTagByNormalizedName(tagname);

            // create a dummy tag if the user has used an actual tag name 
            // in the url and we are unable to find it
            if (tag == null)
                tag = new Tag { TagName = tagname, NormalizedTagName = tagname };

            IList<PostSummary> postSummaryList = Mapper.Map<IList<Post>, PostSummary[]>(posts);

            PostsByTag pBT = new PostsByTag { Posts = postSummaryList, Tag = tag, TotalPages = totalPages};

            
            return View(pBT);
        }

        protected virtual ActionResult ViewPost(int id, CommentDetails commentDetails)
        {
            Post p = postServices.GetPostById(id);

            PostDetails details = Mapper.Map<Post, PostDetails>(p);

            details.EditedComment = commentDetails;

            return View(MVC.Posts.Views.ViewPost, details);
        }

        [HttpPost,ValidateInput(false)]
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

        [HttpPost, Authorize]
        public virtual ActionResult DeleteComment(int commentId)
        {
            commentService.DeleteComment(commentId);
            return Json(new {Success = true});
        }

        [HttpPost, Authorize]
        public virtual ActionResult UndeleteComment(int commentId)
        {
            commentService.UndeleteComment(commentId);
            return Json(new { Success = true });
        }

        public virtual ActionResult RssFeed()
        {
            IList<Post> posts = postServices.GetLatestPosts(Convert.ToInt32(config.RssPostCount));

            PostSummary[] postList = Mapper.Map<IList<Post>, PostSummary[]>(posts);

            return View(postList);
        }
    }
}
