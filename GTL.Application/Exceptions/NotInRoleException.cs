using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NotInRoleException : Exception
    {
        public NotInRoleException()
            : base("No user role found in httpcontext")
        {
        }
    }
}
