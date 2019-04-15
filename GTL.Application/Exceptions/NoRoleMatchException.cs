using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NoRoleMatchException : Exception
    {
        public NoRoleMatchException(string role)
            : base($"Failed to map role \"{role}\" to an entry in the database")
        {
        }
    }
}
