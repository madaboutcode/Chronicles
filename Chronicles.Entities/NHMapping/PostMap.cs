using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Chronicles.Entities.NHMapping
{
    public class PostMap:ClassMap<Post>
    {
        public PostMap()
        {
            Table("Posts");
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Body);
            Map(x => x.UserId);
            Map(x => x.CreateDate);
            Map(x => x.ModifiedDate);
            Map(x => x.ScheduledDate);
            Map(x => x.Approved);
            HasMany(x => x.Comments).Where(x=>x.Deleted==0).KeyColumn("PostId").Inverse();
            HasManyToMany(x => x.Tags)
                .Table("PostsTags")
                .ParentKeyColumn("PostId")
                .ChildKeyColumn("TagId")
                .Not.LazyLoad();
        }
    }
}