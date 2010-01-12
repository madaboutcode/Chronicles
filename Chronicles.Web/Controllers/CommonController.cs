using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Chronicles.Framework;
using Chronicles.Services;
using Chronicles.Web.ViewModels;
using log4net;

namespace Chronicles.Web.Controllers
{
    public class CommonController : BaseController
    {
        private PostServices postService;
        private AppConfiguration config;

        public CommonController(PostServices postService, AppConfiguration config, ILog logger)
            : base(logger)
        {
            this.postService = postService;
            this.config = config;
        }
        public ActionResult RecentPosts()
        {
            PostSummary [] recentPosts = postService.GetLatestPosts(config.)
            return View();
        }

    }
}
