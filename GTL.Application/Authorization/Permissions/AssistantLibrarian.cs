using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.Authorization.Permissions
{
    public class AssistantLibrarian<T> : IAuthorizer, IRequest<T>
    {
        public async Task<bool> Evaluate(User user, CancellationToken cancellationToken)
        {
            return user.Role.PermissionLevel > 20;
        }
    }
}
