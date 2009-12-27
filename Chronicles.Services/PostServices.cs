using System;
using System.Collections.Generic;
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
            StringDictionary addedTags = new StringDictionary();

            foreach (var tag in tags)
            {
                if (string.IsNullOrEmpty(tag.TagName))
                    throw new ConstraintViolationException("Tags should not be empty");

                tag.Id = 0;

                if (tag.DateCreated < DateTime.Now)
                    tag.DateCreated = DateTime.Now;

                post.Tags.Remove(tag);

                if (!addedTags.ContainsKey(tag.TagName))
                {
                    Tag tagFromRepo = tagRepository.GetTagByName(tag.TagName);

                    post.AddTag(tagFromRepo ?? tag);
                    addedTags.Add(tag.TagName, tag.TagName);
                }
            }
        } 
        #endregion

        public Post GetPostById(int id)
        {
            return postRepository.GetPostById(id);
        }
    }
}
