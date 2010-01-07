using System;
using Chronicles.Entities;
using System.Collections.Generic;


namespace Chronicles.DataAccess.Facade
{
    public interface ITagRepository
    {
        Tag GetTagByName(string tagName);
        Tag GetTagByNormalizedName(string normalizedName);
        IList<Tag> GetAll();
    }
}
