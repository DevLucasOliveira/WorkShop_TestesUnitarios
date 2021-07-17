using System;
using System.Linq.Expressions;
using WorkShop.Entities;

namespace WorkShop.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> GetByEmail(string email)
        {
            return x => x.Email == email;
        }

        public static Expression<Func<User, bool>> GetById(Guid userId)
        {
            return x => x.Id == userId;
        }

    }
}
