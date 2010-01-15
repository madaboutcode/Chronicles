using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using Chronicles.Entities;
using Chronicles.DataAccess.Context;
using NHibernate;
using Chronicles.DataAccess.Facade;
using Chronicles.Framework;

namespace Chronicles.DataAccess
{
    public class PostRepository : RepositoryBase, IPostRepository
    {
        AppConfiguration config;
        ITagRepository tagRepository;
        public PostRepository(ITagRepository tagRepository, AppConfiguration config, DbContext ctx)
            : base(ctx)
        {
            this.config = config;
            this.tagRepository = tagRepository;
        }

        public IList<Post> GetLatestPosts(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count", "count must be > 0");

            var query = from post in DbContext.Posts
                        where post.ScheduledDate <= DateTime.Now && post.Approved == true
                        orderby post.ScheduledDate descending
                        select new { Post = post, CommentCount = post.Comments.Count() };

            List<Post> posts = new List<Post>(count);

            var result = query.Take(count);

            foreach (var row in result)
            {
                row.Post.CommentCount = row.CommentCount;
                posts.Add(row.Post);
            }

            return posts;
        }

        public Post AddPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(post);
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

        public IList<Post> GetPostsByTag(string name, int pageSize, int pagenumber, out int totalRows)
        {
            var posts = from tag in DbContext.Tags
                        from post in DbContext.Posts
                        where (tag.TagName == name || tag.NormalizedTagName == name)
                            && post.Tags.Contains(tag)
                        orderby post.ScheduledDate descending
                        select new { Post = post, CommentCount = post.Comments.Count() };

            totalRows = posts.Count();

            int pagePostStartIndex = (pagenumber - 1) * pageSize;

            if (pagePostStartIndex > 0)
            {
                posts = posts.Skip(pagePostStartIndex);
            }

            var result = posts.Take(pageSize).ToList();
            List<Post> postsForDisplay = new List<Post>(totalRows);

            foreach (var row in result)
            {
                row.Post.CommentCount = row.CommentCount;
                postsForDisplay.Add(row.Post);
            }

            return postsForDisplay;
        }

    }
}
