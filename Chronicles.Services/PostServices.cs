using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.Framework;
using System.Collections.Specialized;

namespace Chronicles.Services
{
    public class PostServices
    {
        private IPostRepository postRepository;
        private ITagRepository tagRepository;
        private AppConfiguration config;

        public PostServices(IPostRepository postRepository, ITagRepository tagRepository, AppConfiguration config)
        {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
            this.config = config;
        }

        public IList<Post> GetLatestPosts()
        {
            return GetLatestPosts(Convert.ToInt32(config.NoOfHomePagePosts));
        }

        public IList<Post> GetLatestPosts(int count)
        {
            return postRepository.GetLatestPosts(count);
        }

        #region AddPost
        public Post AddPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            SetupPostDefaults(post);
            ProcessTagsInPost(post);

            post = postRepository.AddPost(post);
            return post;
        }

        private void SetupPostDefaults(Post post)
        {
            post.Id = 0;
            post.ModifiedDate = DateTime.Now;
            post.CreateDate = DateTime.Now;
            post.UserId = 1; //TODO: remove this hardcoding once we add the user management

            if (post.ScheduledDate < DateTime.Now)
                post.ScheduledDate = DateTime.Now;
        }

        private void ProcessTagsInPost(Post post)
        {
            if (post.Tags == null || post.Tags.Count == 0)
                throw new ConstraintViolationException("Every post should have at least one tag attached to it");

            IList<Tag> tags = new List<Tag>(post.Tags);

            foreach (var tag in tags)
            {
                if (string.IsNullOrEmpty(tag.TagName))
                    throw new ConstraintViolationException("Tags should not be empty");

                //tag.Id = 0;
                if (tag.Id == 0)
                {
                    if (tag.DateCreated < DateTime.Now)
                        tag.DateCreated = DateTime.Now;

                    tag.NormalizedTagName = StringUtility.GetNormalizedText(tag.TagName, '_');

                    post.Tags.Remove(tag);

                    Tag tagFromRepo = tagRepository.GetTagByName(tag.TagName);

                    post.AddTag(tagFromRepo ?? tag);
                }
            }
        } 
        #endregion

        #region Edit Post

        //TODO: Tests!
        public Post UpdatePost(Post postToSave)
        {
            if (postToSave == null)
                throw new ArgumentNullException("postToSave");

            if(postToSave.Id == 0)
                throw new DataException("A post to update should have a valid Id");

            ProcessTagsInPost(postToSave);

            postToSave.ModifiedDate = DateTime.Now;

            postToSave = postRepository.AddPost(postToSave);
            return postToSave;
        }

        #endregion Edit Post

        public Post GetPostById(int id)
        {
            return postRepository.GetPostById(id);
        }

        //TODO: unit test
        public IList<Post> GetPostsByTag(string tagName, int pageSize, int pageNumber, out int totalPages)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("tagName should be a valid value");

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pagesize must be greater than 0");

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber must be greater than zero");

            int totalRows;
            
            IList<Post> posts = postRepository.GetPostsByTag(tagName, pageSize, pageNumber, out totalRows);

            if (totalRows > 0)
            {
                totalPages = Convert.ToInt32(Math.Ceiling(totalRows * 1.0 / pageSize));
            }
            else totalPages = 0;

            return posts;
        }
    }
}
