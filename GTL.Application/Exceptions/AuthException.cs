using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;

namespace GTL.Application.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(PermissionLevel userLevel, PermissionLevel requiredLevel, string errorMessage)
            : base($"User with permissionLevel \"{userLevel}\" did not have required permissionLevel \"{requiredLevel}\". - {errorMessage}")
        {
        }
    }
}
