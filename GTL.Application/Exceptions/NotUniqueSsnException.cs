using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NotUniqueSsnException : Exception
    {
        public NotUniqueSsnException(string ssn)
            : base($"Member with ssn \"{ssn}\" already exists.")
        {
        }
    }
}
