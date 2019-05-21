using GTL.Application.Features.Login.Commands;
using System.Threading.Tasks;
using Dapper;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Domain.Entities;
using Xunit;

namespace IntegrationTests
{
    public class RestrictedLoanMaterialTests : IntegrationBase
    {
        public RestrictedLoanMaterialTests()
        {
            SeedDatabase();
        }

        [Fact(DisplayName = "CanLoanCopyWithAvailableStatus")]
        public async Task TDS_1_TC2_5()
        {
            // Arrange
            var command = new CreateLoanCommand
            {
               LoanerCardBarcode  = 10000,
               CopyBarcode = 100001,
               LibraryName = "Georgia Tech Library"
            };

            // Act
            var response = await _mediator.Send(command);

            Loan insertedLoan;
            // Assert
            var query = $"SELECT * FROM Loan WHERE Id = {response.SubjectId}";
            using (var cmd = _context.CreateCommand())
            {
                insertedLoan = cmd.Connection.QuerySingle<Loan>(query);
            }

            Assert.NotNull(insertedLoan);
        }
    }
}
