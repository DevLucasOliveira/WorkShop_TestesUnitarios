using WorkShop.ValueObjects;

namespace WorkShop.Entities
{
    public class User : Entity
    {
        public User(string name, string email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public Password Password { get; set; }
    }
}
