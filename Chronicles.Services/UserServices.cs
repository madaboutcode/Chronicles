using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.Framework;

namespace Chronicles.Services
{
    public class UserServices
    {
        private IUserRepository userRepository;
        private AppConfiguration config;

        public UserServices()
        {
            
        }

        public UserServices(IUserRepository userRepository, AppConfiguration config)
        {
            this.userRepository = userRepository;
            this.config = config;
        }

        public virtual bool AuthenticateUser(string userName, string password)
        {
            if (string.Compare(userName, "username", true) == 0 &&
                password == "P4$5w0rd")
            {
                return true;
            }
            return false;
        }

        public virtual User GetNewOrExistingUser(User user)
        {
            if (user == null) throw new ArgumentNullException("user");


            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("User should have a valid email");

            User existingUser = userRepository.GetUserByEmail(user.Email);

            if(existingUser == null)
            {
                user.Id = 0;
                user.Role = UserRole.Visitor;
                user.DateCreated = DateTime.Now;
                return user;
            }
            else
            {
                if (user.Name != existingUser.Name)
                    existingUser.Name = user.Name;

                if (user.WebSite != existingUser.WebSite)
                    existingUser.WebSite = user.WebSite;

                return existingUser;
            }
        }
    }
}
