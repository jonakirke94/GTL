using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CreateLoanerCardTests
    {
        [Theory]
        [InlineData("123")]
        public async Task LoanerCardWasCreated(string ssn)
        {
            // Arrange
            var loanerCardRepo = new Mock<ILoanerCardRepository>();

            var command = new Mock<CreateLoanerCardCommand>();
            command.Object.Ssn = ssn;
            var sut = new CreateLoanerCardHandler(loanerCardRepo.Object);

            // Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            loanerCardRepo.Verify(x => x.Add(It.IsAny<LoanerCard>()), Times.Once());
        }

        [Theory]
        [InlineData("123")]
        public async Task DeactivedExistingActiveCard(string ssn)
        {
            // Arrange
            var loanerCardRepo = new Mock<ILoanerCardRepository>();
            var personRepo = new Mock<IMemberRepository>();
            var fakeLoanerCard = new Mock<LoanerCard>();
            fakeLoanerCard.Object.Barcode = "FakeBarCode";
            fakeLoanerCard.Object.IsActive = true;
            var list = new List<LoanerCard>
            {
                fakeLoanerCard.Object
            };

            loanerCardRepo.Setup(x => x.GetLoanerCardsBySsn(It.IsAny<string>())).Returns(list);

            var command = new Mock<CreateLoanerCardCommand>();
            command.Object.Ssn = ssn;

            var sut = new CreateLoanerCardHandler(loanerCardRepo.Object);

            // Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            loanerCardRepo.Verify(x => x.DeactiveLoanerCard(It.IsAny<string>()), Times.Once());
        }

        [Theory]
        [InlineData("123")]
        public async Task DidntDeactiveWithNoActive(string ssn)
        {
            // Arrange
            var loanerCardRepo = new Mock<ILoanerCardRepository>();
            var fakeLoanerCard = new Mock<LoanerCard>();
            fakeLoanerCard.Object.Barcode = "FakeBarCode";
            fakeLoanerCard.Object.IsActive = false;
            var list = new List<LoanerCard>
            {
                fakeLoanerCard.Object
            };

            loanerCardRepo.Setup(x => x.GetLoanerCardsBySsn(It.IsAny<string>())).Returns(list);

            var command = new Mock<CreateLoanerCardCommand>();
            command.Object.Ssn = ssn;

            var sut = new CreateLoanerCardHandler(loanerCardRepo.Object);

            // Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            loanerCardRepo.Verify(x => x.DeactiveLoanerCard(It.IsAny<string>()), Times.Never());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowOnEmptySsn(string ssn)
        {
            var _validator = new CreateLoanerCardCommandValidator();

            var command = new Mock<CreateLoanerCardCommand>();
            command.Object.Ssn = ssn;

            _validator.ShouldHaveValidationErrorFor(user => user.Ssn, command.Object);
        }

        [Fact]
        public void ShouldNotThrowOnValid()
        {
            var ssn = "123";
            var _validator = new CreateLoanerCardCommandValidator();

            var command = new Mock<CreateLoanerCardCommand>();
            command.Object.Ssn = ssn;

            _validator.ShouldNotHaveValidationErrorFor(user => user.Ssn, command.Object);
        }
    }
}

