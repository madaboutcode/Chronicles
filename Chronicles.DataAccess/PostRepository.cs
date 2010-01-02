﻿using System;
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

            var result = (from post in DbContext.Posts
                          where post.ScheduledDate <= DateTime.Now && post.Approved == true
                          orderby post.ScheduledDate descending 
                          select new { Post = post, CommentCount = post.Comments.Count() }).Take(count);

            List<Post> posts = new List<Post>(result.Count());

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

        //TODO: make this logic work.. nhibernate limitation.. see [http://www.mail-archive.com/nhcdevs@googlegroups.com/msg00333.html]
        /*
        public IList<Post> GetPostsByTag(string name, int pageSize, int pagenumber, out int totalRows)
        {
            var posts = from tag in DbContext.Tags
                        from post in DbContext.Posts
                        where tag.TagName == name && post.Tags.Contains(tag)
                        orderby post.ScheduledDate descending
                        select post;

            if (posts != null)
            {
                totalRows = posts.Count();

                IQueryable<Post> postsForDisplay = posts;

                int pagePostStartIndex = (pagenumber - 1) * pageSize;

                if (pagePostStartIndex > 0)
                {
                    postsForDisplay = posts.Skip(pagePostStartIndex);
                }

                return postsForDisplay.Take(pageSize).ToList();
            }
            else
            {
                totalRows = 0;
                return new List<Post>();
            }
        }*/

        public IList<Post> GetPostsByTag(string tagname, int pageSize, int pagenumber, out int totalRows)
        {
            Tag tag = tagRepository.GetTagByName(tagname);

            if (tag != null)
            {
                var posts = (from post in tag.Posts 
                            where post.ScheduledDate <= DateTime.Now && post.Approved == true
                            orderby post.ScheduledDate descending
                            select post).ToList();

                if (posts != null && posts.Count > 0)
                {
                    totalRows = posts.Count;

                    IEnumerable<Post> postsForDisplay = posts.Take(pageSize);

                    int pagePostStartIndex = (pagenumber - 1) * pageSize;

                    if (pagePostStartIndex > 0)
                    {
                        postsForDisplay = postsForDisplay.Skip(pagePostStartIndex);
                    }

                    return postsForDisplay.ToList();
                }
            }
            totalRows = 0;
            return new List<Post>();
        }

    }
}
