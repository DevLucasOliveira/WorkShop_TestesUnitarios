using WorkShop.ValueObjects;

namespace WorkShop.Entities
{
    public class User : Entity
    {
        public User() { }
        public User(string name, string email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
            CompletedRegistration = true;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool CompletedRegistration { get; set; }
        public Password Password { get; set; }
    }
}
