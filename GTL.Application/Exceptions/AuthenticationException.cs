using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Enums;

namespace GTL.Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base("User is not authenticated.")
        {
        }
    }
}
