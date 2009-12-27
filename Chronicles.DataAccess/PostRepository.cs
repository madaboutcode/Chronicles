using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using Chronicles.Entities;
using Chronicles.Entities.Context;
using NHibernate;
using Chronicles.DataAccess.Facade;
using Chronicles.Framework;

namespace Chronicles.DataAccess
{
    public class PostRepository:RepositoryBase, IPostRepository
    {
        AppConfiguration config;   
        public PostRepository(AppConfiguration config, DbContext ctx) : base(ctx)
        {
            this.config = config;
        }

        public IList<Post> GetLatestPosts(int count)
        {
            if(count <= 0)
                throw new ArgumentOutOfRangeException("count", "count must be > 0");

            var result = (from post in DbContext.Posts
                         where post.ScheduledDate <= DateTime.Now && post.Approved == true
                         orderby post.ScheduledDate descending
                         select post).Take<Post>(count);
            
            return result.ToList<Post>();
        }

        public Post AddPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            using (ITransaction transaction = DbContext.Session.BeginTransaction())
            {
                DbContext.Session.SaveOrUpdate(post);
                transaction.Commit();
            }

            return post;
        }

        public Post GetPostById(int id)
        {
            return (from post in DbContext.Posts
                    where post.Id == id
                    select post).FirstOrDefault<Post>(); 
        }
    }
}
