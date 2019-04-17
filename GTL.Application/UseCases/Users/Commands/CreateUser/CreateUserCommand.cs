using GTL.Domain.Entities;
using GTL.Domain.Entities.Identity;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }   
        
        public PermissionLevel? PermissionLevel { get; set; }


    }
}


