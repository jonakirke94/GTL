using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    { 
        public int Id { get; set; }
    }
}
