using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces
{
    public interface ILoanHelper
    {
        bool IsLoanerCardActive(string barcode);

        DateTime GetDueDateByMemberType(string barcode, string libraryName);
    }
}
