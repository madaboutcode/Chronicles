using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronicles.Services;
using Chronicles.Entities;
using AutoMapper;
using Chronicles.Web.ViewModels;

namespace Chronicles.Web.Controllers
{
    public partial class ArchivesController : Controller
    {
        PostServices postServices;

        public ArchivesController(PostServices postServices)
        {
            this.postServices = postServices;
        }
        //
        // GET: /Archives/

        public virtual ActionResult ViewPost(int year, int month, int day, int id, string title)
        {
            Post p = postServices.GetPostById(id);

            //TODO: 
            //if(p == null)
            //return View("PostNotFound");

            return View(Mapper.Map<Post, PostSummary>(p));
        }

    }
}
