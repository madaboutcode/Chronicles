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
    public partial class CommentController : BaseController
    {
        CommentServices commentService;
        AppConfiguration config;

        public CommentController(CommentServices commentService, AppConfiguration config, ILog logger) : base(logger)
        {
            this.commentService = commentService;
            this.config = config;
        }
    }
}
