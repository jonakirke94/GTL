using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;

namespace GTL.Persistence.Repositories
{
    public class LoanerCardRepository : ILoanerCardRepository
    {
        protected readonly IGTLContext _context;

        private DataBaseSettings Options { get; }

        public LoanerCardRepository(IGTLContext context)
        {
            _context = context;
        }

        public LoanerCard GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        public void DeactiveLoanerCard(string barcode)
        {
            throw new NotImplementedException();
        }
    }
}
