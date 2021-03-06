﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.Entities;
using Chronicles.DataAccess.Facade;
using Chronicles.Framework;
using Chronicles.DataAccess.Context;

namespace Chronicles.DataAccess
{
    public class TagRepository:RepositoryBase, ITagRepository
    {
        AppConfiguration config;
        public TagRepository(AppConfiguration config, DbContext ctx): base(ctx)
        {
            this.config = config;
        }

        public Tag GetTagByName(string tagName)
        {
            return (from tag in DbContext.Tags
                    where tag.TagName == tagName
                    select tag).FirstOrDefault<Tag>();
        }

        public Tag GetTagByNormalizedName(string normalizedName)
        {
            return (from tag in DbContext.Tags
                    where tag.NormalizedTagName == normalizedName
                    select tag).FirstOrDefault<Tag>();
        }

        public IList<Tag> GetAll()
        {
            return (from tag in DbContext.Tags
                    orderby tag.TagName descending
                    select tag).ToList<Tag>();
        }
    }
}
