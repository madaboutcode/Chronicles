using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronicles.Services;
using Chronicles.Framework;
using Chronicles.Entities;
using Chronicles.Web.ViewModels;
using Chronicles.Web.Utility;
using log4net;

namespace Chronicles.Web.Controllers
{
    [HandleError]
    public partial class CommentController : BaseController
    {
        CommentServices commentService;
        AppConfiguration config;

        public CommentController(CommentServices commentService, AppConfiguration config, ILog logger) : base(logger)
        {
            this.commentService = commentService;
            this.config = config;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AddComment(CommentDetails commentDetails)
        {
            if (ModelState.IsValid)
            {
                /*CommentDetails commentDetails = new CommentDetails();
                UpdateModel<CommentDetails>(commentDetails, form);*/
                Comment comment = GetComment(commentDetails);

                commentService.AddComment(comment, commentDetails.PostId);

                return RedirectToAction(comment.Post.GetActionUrl()); 
            }
            else
                return View();
        }

        private Comment GetComment(CommentDetails commentDetails)
        {
            Comment comment = new Comment { Text = commentDetails.Text };
            User user = new User { Email = commentDetails.UserEmail, Name = commentDetails.UserName };
            comment.User = user;

            return comment;
        }

    }
}
