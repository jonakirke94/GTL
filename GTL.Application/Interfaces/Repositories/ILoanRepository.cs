using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;

namespace GTL.Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        void Add(Loan loan);
    }
}
