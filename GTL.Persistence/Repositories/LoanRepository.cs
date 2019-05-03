using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        protected readonly IGTLContext _context;

        private DataBaseSettings Options { get; }

        public LoanRepository(IGTLContext context)
        {
            _context = context;
        }

        public void createLoan(Loan loan)
        {
          /*  connection.Execute($@"INSERT INTO [LoanerCard] ([IssueDate], [IsActive], [MemberSsn])
                    VALUES (@{nameof(loanerCard.IssueDate)}, @{nameof(loanerCard.IsActive)}, @{nameof(loanerCard.MemberSsn)});",
                loanerCard); */
            var query = $@"INSERT INTO LOAN [Loan] ([LoanDate], [DueDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (@loanDate, @dueDate, @memberSsn, @copyBarcode, @libraryName)";
            using (var cmd = _context.CreateCommand())
            {
                var param = new DynamicParameters();
                param.Add("@loanDate", loan.LoanDate);
                param.Add("@dueDate", loan.DueDate);
                param.Add("@memberSsn", loan.MemberSsn);
                param.Add("@copyBarcode", loan.CopyBarcode);
                param.Add("@libraryName", loan.LibraryName);

                cmd.Connection.Execute(query, param, cmd.Transaction);
            }
        }

        public int GetAllActiveLoans(string ssn)
        {
            var query = $@"SELECT COUNT (*) as numberOfLoans FROM Loan WHERE MemberSsn = @ssn AND returnDate != null";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var numberOfLoans = connection.Execute(query, new { ssn });
                return numberOfLoans;
            }
        }
    }
}
