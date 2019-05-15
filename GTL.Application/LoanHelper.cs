using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces;

namespace GTL.Application
{
    public class LoanHelper : ILoanHelper
    {
        public bool IsLoanerCardActive(string barcode)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDueDateByMemberType(string barcode, string libraryName)
        {
            throw new NotImplementedException();
        }
    }
}
