using System.Data.SqlClient;
using Dapper;
using Xunit;

namespace GTL.IntegrationTests
{
    public class RestrictedMaterialLoanTests : DatabaseFixture
    {

        [Fact]
        public void ThrowsSqlExceptionOnInvalidCopyStatus()
        {
            var exceptionProcedure = string.Empty;

                const string member = "INSERT INTO [Member] ([Ssn], [Name], [Email], [Type]) VALUES('1234567891', 'FAKE', 'fake@fake.dk', 'PROFESSOR')";
                Cmd.Connection.Execute(member);

                const string library = "INSERT INTO [Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod]," +
                                       " [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES('FAKELIBRARY', 0 ,0 ,0 ,0 ,0, 1)";
                Cmd.Connection.Execute(library);

                const string book = "INSERT INTO [Material] ([ISBN], [Title], [Edition]) VALUES('1234567890', 'FAKETITLE', 0)";
                Cmd.Connection.Execute(book);

                const string copy = "INSERT INTO [Copy] ([Barcode], [Status], [MaterialISBN]) VALUES('12345', 'SOLD', '1234567890')";
                Cmd.Connection.Execute(copy);

                try
                {
                    const string loan = "INSERT INTO [Loan] ([LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) " +
                                        "VALUES(GETDATE(), GETDATE(), GETDATE(), '1234567891', '12345', 'FAKELIBRARY')";

                    Cmd.Connection.Execute(loan);
                }
                catch (SqlException e)
                {
                    exceptionProcedure = e.Procedure;
                }

            Assert.Equal("CHECK_LOANABLE", exceptionProcedure);
        }


        [Fact]
        public void CanLoanCopyWithAvailableStatus()
        {
            const string member = "INSERT INTO [Member] ([Ssn], [Name], [Email], [Type]) VALUES('1234567891', 'FAKE', 'fake@fake.dk', 'PROFESSOR')";
            Cmd.Connection.Execute(member);

            const string library = "INSERT INTO [Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod]," +
                                   " [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES('FAKELIBRARY', 0 ,0 ,0 ,0 ,0, 1)";
            Cmd.Connection.Execute(library);

            const string book = "INSERT INTO [Material] ([ISBN], [Title], [Edition]) VALUES('1234567890', 'FAKETITLE', 0)";
            Cmd.Connection.Execute(book);

            const string copy = "INSERT INTO [Copy] ([Barcode], [Status], [MaterialISBN]) VALUES('12345', 'AVAILABLE', '1234567890')";
            Cmd.Connection.Execute(copy);

            const string loan = "INSERT INTO [Loan] ([LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) " +
                                "VALUES(GETDATE(), GETDATE(), GETDATE(), '1234567891', '12345', 'FAKELIBRARY')";

            Cmd.Connection.Execute(loan);
            var count = Cmd.Connection.ExecuteScalar<int>("SELECT COUNT(*) FROM LOAN");


            Assert.Equal(1, count);
        }

    }
}
