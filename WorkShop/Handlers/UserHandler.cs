using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkShop.Commands;
using WorkShop.DTOs;
using WorkShop.Entities;
using WorkShop.Repositories;
using WorkShop.ValueObjects;

namespace WorkShop.Handlers
{
    public class UserHandler :
        IRequestHandler<LoginUserCommand, RequestResult>,
        IRequestHandler<RegisterUserCommand, RequestResult>,
        IRequestHandler<UpdatePasswordUserCommand, RequestResult>,
        IRequestHandler<CompleteRegistrationUserCommand, RequestResult>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RequestResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications.ToList());

            var user = _userRepository.GetUser(command.Email);
            if (user == null)
                return new RequestResult(false, "Usuário não cadastrado");

            if (user.IsActive == false)
                return new RequestResult(false, "Conta inativa");

            if (user.CompletedRegistration == false)
                return new RequestResult(false, "Cadastro incompleto", command.Email);

            var password = Password.VerifyPasswordHash(command.Password, user.Password.PasswordHash, user.Password.PasswordSalt);
            if (!password)
                return new RequestResult(false, "Senha incorreta", command.Password);

            return new RequestResult(true, "Usuário autenticado com sucesso", (UserDTO)user);
        }

        public async Task<RequestResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications);

            var user = _userRepository.GetUser(command.Email);
            if (user != null)
                return new RequestResult(false, "Usuário já cadastrado no sistema", (UserDTO)user);

            user = new User("Usuario1", "Usuario1@email.com", new Password("Senha123"));

            _userRepository.Create(user);

            return new RequestResult(true, "Usuário cadastrado com sucesso", (UserDTO)user);
        }

        public async Task<RequestResult> Handle(UpdatePasswordUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications);

            var user = _userRepository.GetUser(command.Email);
            if (user == null)
                return new RequestResult(false, "Usuário não cadastrado");

            if (user.IsActive == false)
                return new RequestResult(false, "Conta inativa");

            var password = Password.VerifyPasswordHash(command.OldPassword, user.Password.PasswordHash, user.Password.PasswordSalt);
            if (!password)
                return new RequestResult(false, "Senha atual incorreta", command.OldPassword);

            user.Password = new Password(command.NewPassword);

            _userRepository.Update(user);

            return new RequestResult(true, "Senha alterada com sucesso", (UserDTO)user);
        }

        public async Task<RequestResult> Handle(CompleteRegistrationUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications);

            var user = _userRepository.GetUser(command.Email);
            if (user == null)
                return new RequestResult(false, "Usuário não cadastrado");

            user.Update(command.DocumentCPF, command.DateOfBirth, command.City, command.City);

            user.CompletedRegistration = true;

            _userRepository.Update(user);

            return new RequestResult(true, "Cadastro finalizado com sucesso", (UserDTO)user);
        }

    }
}
