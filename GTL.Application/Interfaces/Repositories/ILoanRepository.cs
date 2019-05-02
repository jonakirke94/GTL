using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        void createLoan(Loan loan);
        int GetAllActiveLoans(string ssn);
    }
}
