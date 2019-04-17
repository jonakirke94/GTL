using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException()
            : base($"No valid id attached to the user was found")
        {
        }
    }
}
