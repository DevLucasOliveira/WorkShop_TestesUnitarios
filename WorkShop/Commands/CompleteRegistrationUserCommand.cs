using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;

namespace WorkShop.Commands
{
    public class CompleteRegistrationUserCommand : Notifiable, IValidatable, IRequest<RequestResult>
    {
        public string Email { get; set; }
        public string DocumentCPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
               .Requires()
               .IsEmail(Email, "Email", "Email inválido")
               .IsNotNullOrEmpty(DocumentCPF, "DocumentCPF", "CPF inválido")
               .IsNotNullOrEmpty(City, "City", "A cidade não pode ser vazia")
               .IsNotNullOrEmpty(Country, "Country", "O país não pode ser vazio"));
        }
    }
}
