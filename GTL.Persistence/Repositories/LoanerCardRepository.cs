using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace GTL.Persistence.Repositories
{
    public class LoanerCardRepository : ILoanerCardRepository
    {
        private DataBaseSettings Options { get; }

        public LoanerCardRepository(IOptions<DataBaseSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
     
        public void CreateLoanerCard(LoanerCard loanerCard)
        {
            
                using (var connection = new SqlConnection(Options.ConnectionString))
                {
                    connection.Open();
                    connection.Execute($@"INSERT INTO [LoanerCard] ([IssueDate], [IsActive], [MemberSsn])
                    VALUES (@{nameof(loanerCard.IssueDate)}, @{nameof(loanerCard.IsActive)}, @{nameof(loanerCard.MemberSsn)});",
                        loanerCard);
                }
            
           


        }

        public void DeactiveLoanerCard(string barcode)
        {
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                connection.Execute($@"Update LoanerCard set IsActive = false WHERE barcode = @barcode", new { barcode });
            }
        }

        public IEnumerable<LoanerCard> GetLoanerCardsBySsn(string ssn)
        {
            var query = $@"SELECT * FROM LOANERCARD WHERE MemberSsn = @ssn";
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                connection.Open();
                var loanerCards = connection.Query<LoanerCard>(query, new { ssn });
                return loanerCards;
            }
        }
    }
}
