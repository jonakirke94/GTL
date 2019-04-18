using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<SignInResult> ValidatePasswordAsync(string email, string password);

        Task<AuthModel> HasMinPermission(PermissionLevel permission, CancellationToken cancellationToken);
    }
}


