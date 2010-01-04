using Chronicles.Entities;

namespace Chronicles.DataAccess.Facade
{
    public interface ICommentRepository
    {
        Comment AddComment(Comment comment);
        Comment UpdateComment(Comment comment);
    }
}