using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class RestrictedLoanMaterialTests
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly Mock<ILoanHelper> _loanHelper;
        private readonly Mock<ICopyRepository> _copyRepo;
        private readonly CreateLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;
        private readonly Copy _fakeCopy;

        public RestrictedLoanMaterialTests()
        {
            _copyRepo = new Mock<ICopyRepository>();
            _loanHelper = new Mock<ILoanHelper>();
            _loanRepo = new Mock<ILoanRepository>();
            _command = new Mock<CreateLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
            _fakeCopy = new Mock<Copy>().Object;
        }

        [Theory(DisplayName = "ReturnsErrorMessageIfNotAllowedForLoan")]
        [InlineData(CopyStatus.SOLD, "The requested copy is not available for loaning Status: SOLD")]
        [InlineData(CopyStatus.AVAILABLE, null)]
        public async Task ReturnsErrorIfNotAllowedForLoan(CopyStatus status, string errorMessage)
        {
            // Arrange
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(true);

            _fakeCopy.Status = status;

            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);

            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            var response = await sut.Handle(_command, default);

            // Assert
            Assert.Equal(errorMessage, response.ErrorMessage);
        }
    }
}
