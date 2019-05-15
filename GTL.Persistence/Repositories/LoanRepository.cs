using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using System.Data;

namespace GTL.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        protected readonly IGTLContext _context;

        public LoanRepository(IGTLContext context)
        {
            _context = context;
        }

        public int Add(Loan loan)
        {
            const string query = "INSERT INTO [Loan] ([LoanDate], [DueDate], [LoanerCardBarcode], [CopyBarcode], [LibraryName]) VALUES (@loanDate, @dueDate, @memberSsn, @copyBarcode, @libraryName); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var cmd = _context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@loanDate", loan.LoanDate);
                param.Add("@dueDate", loan.DueDate);
                param.Add("@memberSsn", loan.LoanerCardBarcode);
                param.Add("@copyBarcode", loan.CopyBarcode);
                param.Add("@libraryName", loan.LibraryName);

                return cmd.Connection.QuerySingle<int>(query, param, cmd.Transaction);
            }
        }

        public int GetAllActive(string ssn)
        {
            var query = $@"SELECT COUNT (*) as numberOfLoans FROM Loan WHERE MemberSsn = @ssn AND returnDate != null";
            using (var cmd = _context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@ssn", ssn);
                var amount = cmd.Connection.Execute(query, param, cmd.Transaction);
                return amount;
            }
        }

        public void Return(string copyBarcode)
        {
            var storedProcedure = "[dbo].[ReturnLoan]";
            using( var cmd = _context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@CopyBarcode", copyBarcode);
                cmd.Connection.Execute(storedProcedure, param, commandType: CommandType.StoredProcedure, transaction: cmd.Transaction);
            }
        }
    }
}
