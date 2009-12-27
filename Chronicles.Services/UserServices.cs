using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Services
{
    public class UserServices
    {
        public UserServices()
        {

        }

        public bool AuthenticateUser(string userName, string password, out string authenticationToken)
        {
            authenticationToken = null;

            if (string.Compare(userName, "username", true) == 0 &&
                password == "P4$5w0rd")
            {
                authenticationToken = "[AUTHENTICATED]";
                return true;
            }
            return false;
        }
    }
}
