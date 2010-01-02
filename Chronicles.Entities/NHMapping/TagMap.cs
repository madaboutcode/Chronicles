using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Chronicles.Entities.NHMapping
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Table("Tags");
            Id(x => x.Id);
            Map(x => x.TagName);
            Map(x => x.DateCreated);
            HasManyToMany(x => x.Posts)
                .Cascade.All()
                .Table("PostsTags")
                .ParentKeyColumn("TagId")
                .ChildKeyColumn("PostId")
                .Inverse();
        }
    }
}
