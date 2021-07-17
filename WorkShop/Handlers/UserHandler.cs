using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkShop.Commands;
using WorkShop.DTOs;
using WorkShop.Repositories;

namespace WorkShop.Handlers
{
    public class UserHandler : IRequestHandler<LoginUserCommand, RequestResult>, IRequestHandler<RegisterUserCommand, RequestResult>
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
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications);

            var user = _userRepository.GetUser(command.Email);
            if (user == null)
                return new RequestResult(false, "Usuário não cadastrado");


            return new RequestResult(true, "Usuário autenticado com sucesso");
        }

        public async Task<RequestResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            command.Validate();

            if (command.Invalid)
                return new RequestResult(false, "Ocorreu um erro interno", command.Notifications);

            var user = _userRepository.GetUser(command.Email);
            if (user != null)
                return new RequestResult(false, "Usuário já cadastrado no sistema", (UserDTO)user);


            return new RequestResult(true, "Usuário cadastrado com sucesso");
        }



    }
}
