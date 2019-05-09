using MediatR;

namespace GTL.Application.Features.Login.Commands
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
