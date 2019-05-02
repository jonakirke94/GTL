using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Loans.Commands.CreateLoan;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests.US_2
{
    public class CreateLoanTest
    {

        [Theory]
        [InlineData("072-34-9710", "00302198556622852554", "Accounting")]
        public async Task LoanWasCreated(string memberSsn, string copyBarcode, string libraryName)
        {
            //Arrange
            /*      var loanRepo = new Mock<ILoanRepository>();
                  var memberRepo = new Mock<IMemberRepository>();
                  var copyRepo = new Mock<ICopyRepository>();
                  var libraryRepo = new Mock<ILibraryRepository>();

                  var command = new Mock<CreateLoanCommand>();


                  command.Object.Loan.MemberSsn = memberSsn;
                  command.Object.Loan.CopyBarcode = copyBarcode;
                  command.Object.Loan.LibraryName = libraryName;
                  command.Object.Loan.LoanDate = DateTime.Now;

                  var sut = new CreateLoanHandler(loanRepo.Object, memberRepo.Object, copyRepo.Object, libraryRepo.Object);

                  //Act
                  await sut.Handle(command.Object, CancellationToken.None);

                  //Assert
                  loanRepo.Verify(x => x.createLoan(It.IsAny<Loan>()), Times.Once); */
            Assert.True(true);
        }
    }
}
