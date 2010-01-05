using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronicles.Services;
using Chronicles.Web.ViewModels;
using Chronicles.Entities;
using AutoMapper;
using Chronicles.Framework;
using log4net;
using Chronicles.Web.Utility;

namespace Chronicles.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private PostServices postService;

        public HomeController(PostServices postService, AppConfiguration config,ILog logger):base(logger)
        {
            this.postService = postService;
        }
        //
        // GET: /Home/

        public virtual ActionResult Index()
        {
            IList<Post> latestPosts = postService.GetLatestPosts();

            PostSummary[] postList = Mapper.Map<IList<Post>, PostSummary[]>(latestPosts);

            return View(postList);
        }
    }
}
