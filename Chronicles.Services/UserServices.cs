using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");

            User user = GetUser(userName);

            if(user == null)
                throw new UnauthorizedAccessException("No user with the given name was found");

            if(user.Role != UserRole.Admin && user.Role != UserRole.Reviewer)
                throw new UnauthorizedAccessException("User is doesn't have permission");

            string passHash = HashPassword(password, user.Salt);

            if(string.Compare(user.Hash, passHash, false) != 0)
                throw new UnauthorizedAccessException("Password is incorrect");

            return true;
        }

        public virtual User GetNewOrExistingVisitor(User user)
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

        public virtual User GetUser(string userName)
        {
            return userRepository.GetUserByName(userName);
        }

        protected virtual string HashPassword(string password, string salt)
        {
            string saltedPasswd = password + salt;

            SHA256 sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.ASCII.GetBytes(saltedPasswd)));
        }
    }
}
