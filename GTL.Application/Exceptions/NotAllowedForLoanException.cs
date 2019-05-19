using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NotAllowedForLoanException : Exception
    {
        public NotAllowedForLoanException(string barcode): base($"Warning! Copy with barcode {barcode} is not available for loan")
        {
                
        }
    }
}
