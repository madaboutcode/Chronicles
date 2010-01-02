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

namespace Chronicles.Web.Controllers
{
    public partial class ArchivesController : Controller
    {
        PostServices postServices;

        public ArchivesController(PostServices postServices, AppConfiguration config)
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
    }
}
