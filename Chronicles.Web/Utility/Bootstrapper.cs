using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using Chronicles.DataAccess.Facade;
using Chronicles.DataAccess;
using Chronicles.Framework;
using AutoMapper;
using Chronicles.Entities;
using Chronicles.Web.ViewModels;

namespace Chronicles.Web.Utility
{
    public static class Bootstrapper
    {
        public static void Boot()
        {
            ConfigureStructureMap();
            ConfigureAutoMapper();
        }

        public static void ConfigureStructureMap()
        {
            StructureMapConfiguration.AddRegistry(new ChroniclesRegistry());
        }

        public static void  ConfigureAutoMapper()
        {
            Mapper.CreateMap<Post, PostSummary>()
                .ForMember(x=>x.PublishedDate, opt => opt.MapFrom(y=>y.ScheduledDate))
                .ForMember(x=>x.Summary, opt => opt.MapFrom(y=>y.Body));
        }
    }

    public class ChroniclesRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IPostRepository>().TheDefaultIsConcreteType<PostRepository>();
            ForRequestedType<ITagRepository>().TheDefaultIsConcreteType<TagRepository>();
            ForRequestedType<IAppConfigProvider>().TheDefaultIsConcreteType<AppConfigProvider>();
        }
    }
}
