using System.Threading.Tasks;
using GTL.Application.Authorization;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Domain.Entities;

namespace GTL.Application.Authentication
{
    public interface IAuthService
    {
        Task<SignInResult> ValidatePasswordAsync(string email, string password);

        bool HasPermission(PermissionLevel permission, User user);
    }
}
