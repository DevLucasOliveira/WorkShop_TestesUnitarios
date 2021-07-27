using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace WorkShop.Commands
{
    public class UpdatePasswordUserCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsEmail(Email, "Email", "Email inválido")
               .IsNotNullOrEmpty(OldPassword, "OldPassword", "A senha não pode ser vazia")
               .IsNotNullOrEmpty(NewPassword, "NewPassword", "A senha não pode ser vazia"));
        }
    }
}
