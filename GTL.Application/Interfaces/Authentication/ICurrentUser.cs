using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated();

        int GetUserId();

        PermissionLevel GetCurrentPermission();
    }
}
