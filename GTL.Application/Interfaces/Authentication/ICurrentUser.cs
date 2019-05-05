using GTL.Domain.Entities;
using GTL.Domain.Enums;

namespace GTL.Application.Interfaces.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated();

        string GetSsn();

        PermissionLevel GetCurrentPermission();
    }
}

