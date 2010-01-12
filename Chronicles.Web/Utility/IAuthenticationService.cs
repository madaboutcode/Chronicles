using System;

namespace Chronicles.Web.Utility
{
    public interface IAuthenticationService
    {
        bool LogIn(string userName, string password, bool persist);
        void LogOut();
    }
}