using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.Framework;

namespace Chronicles.Services
{
    public class CommentServices
    {
        private ICommentRepository commentRepository;
        private AppConfiguration config;
        private UserServices userServices;
        private IPostRepository postRepository;

        public CommentServices(ICommentRepository commentRepository, IPostRepository postRepository, UserServices userServices, AppConfiguration config)
        {
            this.commentRepository = commentRepository;
            this.config = config;
            this.userServices = userServices;
            this.postRepository = postRepository;
        }

        public Comment AddComment(Comment comment, int postId)
        {
            if(postId <= 0)
                throw new ArgumentOutOfRangeException("comment", postId, "should be greater than zero");
            
            Post post = postRepository.GetPostById(postId);

            if (post == null)
                throw new ArgumentOutOfRangeException("postId", postId, "There is no post with that id");

            comment.Post = post;
            return AddComment(comment);
        }

        public Comment AddComment(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("comment");

            if (comment.User == null)
                throw new ArgumentException("All posted comments must have a user attached");

            if (comment.Post == null)
                throw new ArgumentException("All comments added must be attached to a post");

            if(comment.Post.Id <= 0)
                throw  new ArgumentException("All comments must have a valid existing post attached to it");

            SetupCommentDefaults(comment);

            commentRepository.AddComment(comment);

            return comment;
        }

        protected void SetupCommentDefaults(Comment comment)
        {
            comment.Id = 0;
            comment.Date = DateTime.Now;
            comment.Approved = false;
            comment.Deleted = 0;

            comment.User = userServices.GetNewOrExistingVisitor(comment.User);
        }

        public void DeleteComment(int commentId)
        {
            if (commentId <= 0)
                throw new ArgumentOutOfRangeException("commentId");

            Comment comment = commentRepository.GetComment(commentId);

            if(comment == null)
                throw new EntityNotFoundException("A comment with the given id was not found");

            commentRepository.DeleteComment(comment);
        }

        public void DeleteComment(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException("comment");

            if(comment.Id <=0)
                throw new ArgumentException("comment entity is not valid");

            commentRepository.DeleteComment(comment);
        }

        public void UndeleteComment(int commentId)
        {
            if (commentId <= 0)
                throw new ArgumentOutOfRangeException("commentId");

            Comment comment = commentRepository.GetComment(commentId);

            if (comment == null)
                throw new EntityNotFoundException("A comment with the given id was not found");

            commentRepository.UndeleteComment(comment);
        }
    }
}
