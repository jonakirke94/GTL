using GTL.Domain.Entities;

namespace GTL.Application.Infrastructure.RequestModels
{
    public class AuthCommand
    {
        public AuthCommand(PermissionLevel level)
        {
            PermissionLevel = level;
        }

        public PermissionLevel PermissionLevel { get; }
    }
}

