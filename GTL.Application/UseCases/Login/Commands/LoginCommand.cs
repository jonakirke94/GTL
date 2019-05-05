using GTL.Application.Infrastructure.RequestModels;
using MediatR;

namespace GTL.Application.UseCases.Login.Commands
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
