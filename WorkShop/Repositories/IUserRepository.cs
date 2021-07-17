using WorkShop.Entities;

namespace WorkShop.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        User GetUser(string email);
    }
}
