using System.Threading.Tasks;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class LoanLimitTests
    {
        private readonly Mock<ILoanRepository> _loanRepo;
        private readonly Mock<ILoanHelper> _loanHelper;
        private readonly Mock<ICopyRepository> _copyRepo;
        private readonly CreateLoanCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;
        private readonly Copy _fakeCopy;

        public LoanLimitTests()
        {
            _copyRepo = new Mock<ICopyRepository>();
            _loanHelper = new Mock<ILoanHelper>();
            _loanRepo = new Mock<ILoanRepository>();
            _command = new Mock<CreateLoanCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
            _fakeCopy = new Mock<Copy>().Object;
        }

        [Theory(DisplayName = "CanLoanBookWithinLimits")]
        [InlineData(4)]
        [InlineData(0)]
        public void TDS_1_TC25_1(int activeLoans)
        {
            // Arrange
            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(true);
            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);
            _loanRepo.Setup(x => x.GetNoOfActiveLoans(It.IsAny<int>())).Returns(activeLoans);
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            sut.Handle(_command, default);

            // Assert
            _loanRepo.Verify(x => x.Add(It.IsAny<Loan>()), Times.Once());
        }

        [Theory(DisplayName = "ReturnsErrorMessageOnExceededLoanLimit")]
        [InlineData(50)]
        [InlineData(5)]
        [InlineData(-1)]
        [InlineData(-50)]
        public async Task TDS_1_TC25_2(int activeLoans)
        {
            // Arrange
            _loanHelper.Setup(x => x.IsLoanerCardActive(It.IsAny<int>())).Returns(true);
            _copyRepo.Setup(x => x.GetByBarcode(It.IsAny<int>())).Returns(_fakeCopy);
            _loanRepo.Setup(x => x.GetNoOfActiveLoans(It.IsAny<int>())).Returns(activeLoans);

            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
            var sut = new CreateLoanHandler(_context.Object, _loanRepo.Object, _loanHelper.Object, _copyRepo.Object);

            // Act
            var response = await sut.Handle(_command, default);

            // Assert
            Assert.Equal("Member has reached limit of allowed active loans", response.ErrorMessage);

        }


    }
}
