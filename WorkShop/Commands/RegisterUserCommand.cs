using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace WorkShop.Commands
{
    public class RegisterUserCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsNotNullOrEmpty(Name, "Name", "O nome não pode ser vazio")
               .IsEmail(Email, "Email", "Email inválido")
               .IsNotNullOrEmpty(Password, "Password", "A senha não pode ser vazia")
               .IsNotNullOrEmpty(ConfirmPassword, "ConfirmPassword", "A senha não pode ser vazia"));
        }

    }
}
