using GTL.Application.Infrastructure.RequestModels;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.Login
{
    public class LoginCommand : IRequest<SignInResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
