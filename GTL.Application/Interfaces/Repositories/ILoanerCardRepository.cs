using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanerCardRepository
    {
        LoanerCard GetByBarcode(int barcode);

        void DeactiveLoanerCard(int barcode);
    }
}
