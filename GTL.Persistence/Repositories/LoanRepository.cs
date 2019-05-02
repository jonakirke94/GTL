using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;

namespace GTL.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private DataBaseSettings Options { get; }

        public LoanRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public void createLoan(Loan loan)
        {
          /*  connection.Execute($@"INSERT INTO [LoanerCard] ([IssueDate], [IsActive], [MemberSsn])
                    VALUES (@{nameof(loanerCard.IssueDate)}, @{nameof(loanerCard.IsActive)}, @{nameof(loanerCard.MemberSsn)});",
                loanerCard); */
            var query = $@"INSERT INTO LOAN [Loan] ([LoanDate], [DueDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (@{nameof(loan.LoanDate)}, {nameof(loan.DueDate)}, {nameof(loan.MemberSsn)},{nameof(loan.CopyBarcode)}, {nameof(loan.LibraryName)})";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                connection.Execute(query);
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
