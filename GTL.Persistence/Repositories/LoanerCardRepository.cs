using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;

namespace GTL.Persistence.Repositories
{
    public class LoanerCardRepository : ILoanerCardRepository
    {
        protected readonly IGTLContext _context;

        public LoanerCardRepository(IGTLContext context)
        {
            _context = context;
        }

        public LoanerCard GetByBarcode(int barcode)
        {
            const string query = @"SELECT * FROM LoanerCard WHERE Barcode = @barcode";
            using (var cmd = _context.CreateCommand())
            {
                var para = new DynamicParameters();
                para.Add("@barcode", barcode);
                var results = cmd.Connection.Query<LoanerCard>(query, para, cmd.Transaction);
                return results.FirstOrDefault();
            }
        }

        public void DeactiveLoanerCard(int barcode)
        {
            throw new NotImplementedException();
        }
    }
}
