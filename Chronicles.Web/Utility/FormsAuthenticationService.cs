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
            if(userServices.AuthenticateUser(userName, password))
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