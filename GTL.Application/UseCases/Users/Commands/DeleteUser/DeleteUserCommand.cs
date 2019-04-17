using GTL.Application.Authorization;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : AuthCommand, IRequest
    {
        public DeleteUserCommand() : base(PermissionLevel.CHIEFLIBRARIAN)
        {
        }
        
        public int Id { get; set; }
    }
}
