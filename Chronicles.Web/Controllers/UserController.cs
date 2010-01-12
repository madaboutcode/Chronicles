using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using Chronicles.DataAccess;
using Chronicles.Services;
using Chronicles.Web.Utility;
using log4net;

namespace Chronicles.Web.Controllers
{
    public partial class UserController : BaseController
    {
        private readonly IAuthenticationService authenticationService;
        private UserServices userServices;

        public UserController(IAuthenticationService authenticationService, UserServices userServices, ILog log):base(log)
        {
            this.authenticationService = authenticationService;
            this.userServices = userServices;
        }

        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(string username, string password, string returnUrl)
        {
            if(string.IsNullOrEmpty(username))
                ViewData.ModelState.AddModelError("username", "Username is required");

            if (string.IsNullOrEmpty(password))
                ViewData.ModelState.AddModelError("password", "Password is required");

            if(ModelState.IsValid)
            {
                bool isAuthenticated = authenticationService.LogIn(username, password, true);

                if (isAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(username, true);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction(MVC.Home.Index());

                    return Redirect(returnUrl);
                }
                else
                {
                    ViewData.ModelState.AddModelError("common","Invalid username or password!");
                }
            }

            return View();
        }
    }
}

    
