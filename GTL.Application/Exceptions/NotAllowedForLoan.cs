using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Exceptions
{
    public class NotAllowedForLoan : Exception
    {
        public NotAllowedForLoan(string barcode): base($"Warning! Copy with barcode {barcode} is not available for loan")
        {
                
        }
    }
}
