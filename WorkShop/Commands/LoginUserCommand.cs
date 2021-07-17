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
            throw new System.NotImplementedException();
        }

    }
}
