using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.DataAccess.Context;
using Chronicles.Framework;

namespace Chronicles.DataAccess
{
    public class UserRepository:RepositoryBase, IUserRepository
    {
        private AppConfiguration config;
        public UserRepository(AppConfiguration config, DbContext ctx)
            : base(ctx)
        {
            this.config = config;
        }

        public User GetUserByEmail(string email)
        {
            return (from user in DbContext.Users
                   where user.Email == email
                   select user).FirstOrDefault();
        }

        public User GetUserByName(string name)
        {
            return (from user in DbContext.Users
                    where user.Name == name
                    select user).FirstOrDefault();
        }
    }
}
