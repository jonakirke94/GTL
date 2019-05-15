using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        int Add(Loan loan);

        int GetNoOfActiveLoans(int barcode);
    }
}
