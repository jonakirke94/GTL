using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using Moq;
using Xunit;
using GTL.Application.UseCases.Loans.Commands.ReturnLoan;

namespace Application.Tests.US_6
{
    public class ReturnLoanTest
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly ReturnLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;

        public ReturnLoanTest()
        {
            _loanRepo = new Mock<ILoanRepository>();
            _command = new Mock<ReturnLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
        }


        [Theory]
        [InlineData("00000389897175133270")]
        [InlineData("000003898971751")]
        [InlineData("03898971751")]
        public async Task LoanWasCreated(string copyBarcode)
        {
            //Arrange
            _command.CopyBarcode = copyBarcode;

            var sut = new ReturnLoanHandler(_context.Object, _loanRepo.Object);

            //Act
            await sut.Handle(_command, CancellationToken.None);

            //Assert
            _loanRepo.Verify(x => x.Return(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ReturnOfLoanThatDoesnExist()
        {
            //Arrange
            _command.CopyBarcode = "48784894652";

            var sut = new ReturnLoanHandler(_context.Object, _loanRepo.Object);

            //Act
            await sut.Handle(_command, CancellationToken.None);

            //Assert
            _loanRepo.Verify(x => x.Return(It.IsNotIn<string>()), Times.Once);
        }
    }
}
