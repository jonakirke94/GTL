using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Domain.Exceptions
{
    public class ISBNAlreadyExistException : Exception
    {
        public ISBNAlreadyExistException(string ISBN, Exception ex)
            : base($"ISBN \"{ISBN}\" already exist in database.", ex)
        {

        }
    }
}
