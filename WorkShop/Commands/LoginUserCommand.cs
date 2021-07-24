using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace WorkShop.Commands
{
    public class LoginUserCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsEmail(Email, "Email", "O campo email esta inválido")
                           .IsNotNullOrEmpty(Email, "Email", "O email não pode ser vazio")
                           .IsNotNullOrEmpty(Password, "Password", "A senha não pode ser vazia")
                       );
        }

    }
}
