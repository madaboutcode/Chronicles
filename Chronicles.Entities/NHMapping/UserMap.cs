using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Chronicles.Entities.NHMapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.Name);
            Map(x => x.Hash);
            Map(x => x.Salt);
            Map(x => x.DateCreated);
            Map(x => x.Role);
            Map(x => x.WebSite);
            HasMany(x => x.Comments)
                .Inverse()
                .Cascade.All()
                .LazyLoad()
                .KeyColumn("UserId");
        }
    }
}