using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanerCardRepository
    {
        void CreateLoanerCard(LoanerCard loanercard);
        IEnumerable<LoanerCard> GetLoanerCardsBySsn(string ssn);

        void DeactivateLoanerCard(string barcode);
    }
}
