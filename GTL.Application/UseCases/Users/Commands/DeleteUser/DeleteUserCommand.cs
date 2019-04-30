using GTL.Application.Authorization;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IAdminRequest
    { 
        public int Id { get; set; }
    }

    public interface IAdminRequest : IRequest
    {

    }

    public interface IMemberRequest : IRequest
    {

    }
}
