using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;

namespace Chronicles.Entities.NHMapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Table("Comments");
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Text, "Comment");
            Map(x => x.Approved);
            References(x => x.User,"UserId").Cascade.All().LazyLoad();
            References(x => x.Post,"PostId").Cascade.All().LazyLoad();
        }
    }
}