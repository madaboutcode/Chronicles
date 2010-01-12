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
using log4net.Core;
using log4net;
using StructureMap.Attributes;

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

            Mapper.CreateMap<Post, PostDetails>()
                .ForMember(x => x.EditedComment, opt => opt.Ignore())
                .ForMember(x => x.PublishedDate, opt => opt.MapFrom(y => y.ScheduledDate));

            Mapper.CreateMap<Comment, CommentDetails>();

            Mapper.CreateMap<Post, PostTeaser>()
                .ForMember(x => x.PublishedDate, opt => opt.MapFrom(y => y.ScheduledDate));
        }
    }

    public class ChroniclesRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IPostRepository>().TheDefaultIsConcreteType<PostRepository>().CacheBy(InstanceScope.Hybrid);
            ForRequestedType<ITagRepository>().TheDefaultIsConcreteType<TagRepository>().CacheBy(InstanceScope.Hybrid);
            ForRequestedType<IAppConfigProvider>().TheDefaultIsConcreteType<AppConfigProvider>().CacheBy(InstanceScope.Singleton);
            ForRequestedType<ICommentRepository>().TheDefaultIsConcreteType<CommentRepository>().CacheBy(InstanceScope.Hybrid);
            ForRequestedType<IUserRepository>().TheDefaultIsConcreteType<UserRepository>().CacheBy(InstanceScope.Hybrid);
            ForRequestedType<IAuthenticationService>().TheDefaultIsConcreteType<FormsAuthenticationService>().CacheBy(
                InstanceScope.Hybrid);
            ForRequestedType<ILog>()
                .AlwaysUnique()
                .TheDefault
                .Is
                .ConstructedBy(context =>
                {
                    if (context.ParentType == null)
                        return LogManager.GetLogger(context.BuildStack.Current.ConcreteType);

                    return LogManager.GetLogger(context.ParentType);
                });
        }
    }
}
