using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class AuthorizeException : Exception
    {
        public AuthorizeException()
            : base("User is not authorized.")
        {
        }
    }
}
