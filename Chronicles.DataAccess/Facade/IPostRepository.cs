using System;
using Chronicles.Entities;
using System.Collections.Generic;

namespace Chronicles.DataAccess.Facade
{
    public interface IPostRepository
    {
        Post AddPost(Post post);
        IList<Post> GetLatestPosts(int count);
        Post GetPostById(int id);
    }
}
