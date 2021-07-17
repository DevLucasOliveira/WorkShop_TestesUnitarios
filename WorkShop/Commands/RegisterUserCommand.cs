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
            throw new System.NotImplementedException();
        }

    }
}
