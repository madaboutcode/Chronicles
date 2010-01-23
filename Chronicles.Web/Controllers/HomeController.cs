using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using Chronicles.Services;
using Chronicles.Web.Utility;
using Chronicles.Web.ViewModels;
using Chronicles.Entities;
using AutoMapper;
using Chronicles.Framework;
using log4net;

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
        [ChroniclesOutputCache]
        public virtual ActionResult Index()
        {
            IList<Post> latestPosts = postService.GetLatestPosts();

            PostSummary[] postList = Mapper.Map<IList<Post>, PostSummary[]>(latestPosts);

            return View(postList);
        }
    }
}
