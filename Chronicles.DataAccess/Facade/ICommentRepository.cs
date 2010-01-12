using Chronicles.Entities;

namespace Chronicles.DataAccess.Facade
{
    public interface ICommentRepository
    {
        Comment AddComment(Comment comment);
        Comment UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment GetComment(int commentId);
        void UndeleteComment(Comment comment);
    }
}