using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.Framework;

namespace Chronicles.Services
{
    public class TagServices
    {
        private ITagRepository tagRepository;
        private AppConfiguration config;

        public TagServices(ITagRepository tagRepository, AppConfiguration config)
        {
            this.tagRepository = tagRepository;
            this.config = config;
        }

        public IList<Tag> GetAll()
        {
            return tagRepository.GetAll();
        }

        public Tag GetTagByNormalizedName(string name)
        {
            return tagRepository.GetTagByNormalizedName(name);
        }
    }
}
