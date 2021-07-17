using WorkShop.Repositories;

namespace WorkShop.Handlers
{
    public class UserHandler
    {

        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }




    }
}
