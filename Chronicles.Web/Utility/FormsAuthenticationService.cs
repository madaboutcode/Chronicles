using System;
using System.Web.Security;
using Chronicles.Services;

namespace Chronicles.Web.Utility
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        private UserServices userServices;

        public FormsAuthenticationService(UserServices userServices)
        {
            this.userServices = userServices;
        }
        public bool LogIn(string userName, string password, bool persist)
        {
            bool isAuthenticated = false;

            try
            {
                isAuthenticated = userServices.AuthenticateUser(userName, password);
            }
            catch (UnauthorizedAccessException)
            {
                isAuthenticated = false;
            }
            
            if(isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(userName, persist);
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}