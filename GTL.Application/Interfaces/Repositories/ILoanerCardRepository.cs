using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanerCardRepository
    {
        void Add(LoanerCard loanerCard);
        IEnumerable<LoanerCard> GetLoanerCardsBySsn(string ssn);

        void DeactiveLoanerCard(string barcode);
    }
}
