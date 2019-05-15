using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces
{
    public interface ILoanHelper
    {
        bool IsLoanerCardActive(int barcode);

        DateTime GetDueDateByMemberType(int barcode, string libraryName);
    }
}

