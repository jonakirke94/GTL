using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

namespace GTL.Application.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(PermissionLevel permissionLevel)
            : base($"User did not have sufficient permissionLevel. Required: \"{permissionLevel}\"")
        {
        }
    }
}
