using System;
using WorkShop.Entities;

namespace WorkShop.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        User GetUser(string email);
        void Update(User user);
        void Delete(Guid id);
    }
}
