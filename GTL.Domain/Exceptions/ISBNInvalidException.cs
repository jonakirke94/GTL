using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Domain.Exceptions
{
    public class ISBNInvalidException : Exception
    {
        public ISBNInvalidException(string ISBN, Exception ex)
            : base($"ISBN \"{ISBN}\" is invalid.", ex)
        {
        }
    }
}
