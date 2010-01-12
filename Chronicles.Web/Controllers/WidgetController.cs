using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AutoMapper;
using Chronicles.Entities;
using Chronicles.Framework;
using Chronicles.Services;
using Chronicles.Web.ViewModels;
using log4net;

namespace Chronicles.Web.Controllers
{
    public partial class WidgetController : BaseController
    {
        private PostServices postService;
        private AppConfiguration config;

        public WidgetController(PostServices postService, AppConfiguration config, ILog logger)
            : base(logger)
        {
            this.postService = postService;
            this.config = config;
        }
        public virtual ActionResult RecentPosts()
        {
            var posts = postService.GetLatestPosts(Convert.ToInt32(config.RecentPostsBlockCount));
            PostTeaser[] postTeasers = null;

            if(posts!=null)
            {
                postTeasers = Mapper.Map<IList<Post>, PostTeaser[]>(posts);
            }

            return View(postTeasers);
        }

    }
}
