using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NoRoleException : Exception
    {
        public NoRoleException(string name)
            : base($"User with name \"{name}\" couldn't be created without any intial roles.")
        {
        }
    }
}

