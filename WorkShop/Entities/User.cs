using System;
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
        public string DocumentCPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Password Password { get; set; }


        public void Update(string documentCPF, DateTime dateOfBirth, string city, string country)
        {
            DocumentCPF = documentCPF;
            DateOfBirth = dateOfBirth;
            City = city;
            Country = country;
        }
    }
}
