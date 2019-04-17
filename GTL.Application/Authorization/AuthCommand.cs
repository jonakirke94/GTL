using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Authorization
{
    public class AuthCommand
    {
        public AuthCommand(PermissionLevel level)
        {
            PermissionLevel = level;
        }

        public readonly PermissionLevel PermissionLevel;
    }
}

