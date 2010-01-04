using Chronicles.Entities;

namespace Chronicles.DataAccess.Facade
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}