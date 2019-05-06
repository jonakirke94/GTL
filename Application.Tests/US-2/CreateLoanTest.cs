using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Loans.Commands.CreateLoan;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CreateLoanTest
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly Mock<IMemberRepository> _memberRepo;
        private readonly Mock<ICopyRepository> _copyRepo;
        private readonly Mock<ILibraryRepository> _libraryRepo;
        private readonly CreateLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;

        public CreateLoanTest()
        {
            _loanRepo = new Mock<ILoanRepository>();
            _memberRepo = new Mock<IMemberRepository>();
            _copyRepo = new Mock<ICopyRepository>();
            _libraryRepo = new Mock<ILibraryRepository>();
            _command = new Mock<CreateLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
        }


        [Theory]
        [InlineData("072-34-9710", "00302198556622852554", "Accounting", 1, 7, 5, 21)]
        [InlineData("123-45-6789", "12345678998765432154", "SuperCoolLibrary", 4, 7, 5, 21)]
        public async Task LoanWasCreated(string memberSsn, string copyBarcode, string libraryName, int numberOfBooksLended, int gracePeriod, int maxBooksOnLoan, int loanDuration)
        {
            //Arrange
            _command.Loan.MemberSsn = memberSsn;
            _command.Loan.CopyBarcode = copyBarcode;
            _command.Loan.LibraryName = libraryName;
            _command.Loan.LoanDate = DateTime.Now;

            Member fakeMember = new Mock<Member>().Object;
            Library fakeLibrary = new Mock<Library>().Object;
            Copy fakeCopy = new Mock<Copy>().Object;
            Loan fakeLoan = new Mock<Loan>().Object;
            fakeMember.Ssn = memberSsn;
            fakeLibrary.Name = libraryName;
            fakeLibrary.MemberGracePeriod = gracePeriod;
            fakeLibrary.MemberMaxBooksOnLoan = maxBooksOnLoan;
            fakeLibrary.MemberLoanDuration = loanDuration;
            fakeCopy.Barcode = copyBarcode;

            _memberRepo.Setup(x => x.GetBySsn(fakeMember.Ssn)).Returns(fakeMember);
            _libraryRepo.Setup(x => x.GetLibraryByName(fakeLibrary.Name)).Returns(fakeLibrary);
            _copyRepo.Setup(x => x.GetCopyByBarcode(fakeCopy.Barcode)).Returns(fakeCopy);
            _loanRepo.Setup(x => x.GetAllActiveLoans(memberSsn)).Returns(numberOfBooksLended);
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            


            var sut = new CreateLoanHandler(_context.Object,_loanRepo.Object, _memberRepo.Object, _copyRepo.Object, _libraryRepo.Object);

            //Act
            await sut.Handle(_command, CancellationToken.None);

            //Assert
            _loanRepo.Verify(x => x.Add(It.IsAny<Loan>()), Times.Once);
        }

        [Theory]
        [InlineData( "")]
        [InlineData("1234")]
        [InlineData("123456789123")]
        public void LoanValidationShouldThrowOnMemberSsnTest(string memberSsn)
        {
            var validator = new CreateLoanCommandValidator();
            _command.Loan.MemberSsn = memberSsn;

            validator.ShouldHaveValidationErrorFor(loan => loan.Loan.MemberSsn, _command);
        }

        [Theory]
        [InlineData("")]
        public void LoanValidationShouldThrowOnCopyBarcodeTest(string copyBarcode)
        {
            var validator = new CreateLoanCommandValidator();
            _command.Loan.CopyBarcode = copyBarcode;

            validator.ShouldHaveValidationErrorFor(loan => loan.Loan.CopyBarcode, _command);
        }

        [Fact]
        public void LoanValidationShouldThrowOnLoanDateTest()
        {
            var validator = new CreateLoanCommandValidator();
            
            validator.ShouldHaveValidationErrorFor(loan => loan.Loan.LoanDate, _command);
        }

        [Theory]
        [InlineData("")]
        public void LoanValidationShouldThrowOnLibraryNameTest(string libraryName)
        {
            var validator = new CreateLoanCommandValidator();
            _command.Loan.LibraryName = libraryName;

            validator.ShouldHaveValidationErrorFor(loan => loan.Loan.LibraryName, _command);
        }
    }
}
