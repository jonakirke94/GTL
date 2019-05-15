using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Application.Helper;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CreateLoanTest
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly Mock<ILoanHelper> _loanHelper;
        private readonly Mock<ICopyRepository> _copyRepo;
        private readonly CreateLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;
        private readonly Copy _fakeCopy;

        public CreateLoanTest()
        {
            _copyRepo = new Mock<ICopyRepository>();
            _loanHelper = new Mock<ILoanHelper>();
            _loanRepo = new Mock<ILoanRepository>();
            _command = new Mock<CreateLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
            _fakeCopy = new Mock<Copy>().Object;

        }

        [Fact(DisplayName = "LoanWasAdded")]
        public void TDS_1_TC2_1()
        {
            // Arrange
            _command.LoanerCardBarcode = 072-34-9710;
            _command.CopyBarcode = 36554;
            _command.LibraryName = "Georgia Tech Library";

            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(true);
            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);

            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            sut.Handle(_command, default);

            // Assert
            _loanRepo.Verify(x =>x.Add(It.IsAny<Loan>()), Times.Once());
        }

        [Theory(DisplayName = "ShouldValidateLoanData")]
        [InlineData(0,3021, "Georgia Tech Library", false)]
        [InlineData(3021, 0, "Georgia Tech Library", false)]
        [InlineData(3021, 3021, "", false)]
        [InlineData(3021, 3021, "Georgia Tech Library", true)]

        public void TDS_1_TC2_2(int loanerCardBarcode, int copyBarcode, string libraryName, bool expectedResult)
        {
            // Arrange
            var sut = new CreateLoanCommandValidator();

            _command.LoanerCardBarcode = loanerCardBarcode;
            _command.CopyBarcode = copyBarcode;
            _command.LibraryName = libraryName;

            // Act
            var validationRes = sut.Validate(_command);

            // Assert
            Assert.Equal(expectedResult, validationRes.IsValid);
        }

        [Theory(DisplayName = "CheckForActiveLoanercardTest")]
        [InlineData(true, null)]
        [InlineData(false, "The used loanercard is no longer active")]
        public async Task TDS_1_TC2_3(bool activeStatus, string errorMessage)
        {
            // Arrange
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(activeStatus);
            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);

            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            var response = await sut.Handle(_command, default);

            // Assert
            Assert.Equal(errorMessage, response.ErrorMessage);
        }

        [Fact(DisplayName = "CheckDueDateIsBeingCalculated")]
        public void TDS_1_TC2_4()
        {
            // Arrange
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(true);
            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);

            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            sut.Handle(_command, default);

            // Assert
            _loanHelper.Verify(x => x.GetDueDateByMemberType(It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(1));
        }

    }
}
