using System.Threading;
using System.Threading.Tasks;
using GTL.Domain.Entities;

namespace GTL.Application.Authorization
{
    public interface IAuthorizer
    {
        Task<bool> Evaluate(User user, CancellationToken cancellationToken);
    }
}
