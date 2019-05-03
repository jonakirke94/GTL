using System;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard;
using GTL.Application.UseCases.Members.Commands.CreateMember;
using GTL.Application.Exceptions;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Moq;
using Xunit;
using Xunit.Sdk;
using GTL.Application.Interfaces.UnitOfWork;

namespace Application.Tests
{
    public class CreateMemberTest
    {
        private readonly Mock<IMemberRepository> _memberRepo;
        private readonly Mock<ILoanerCardRepository> _loanerCardRepo;
        private readonly Mock<IAddressRepository> _addressRepo;
        private readonly CreateMemberCommand _command;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IGTLContext> _context;

        public CreateMemberTest()
        {
            _memberRepo = new Mock<IMemberRepository>();
            _loanerCardRepo = new Mock<ILoanerCardRepository>();
            _addressRepo = new Mock<IAddressRepository>();
            _command = new Mock<CreateMemberCommand>().Object;
            _context = new Mock<IGTLContext>();
            _uow = new Mock<IUnitOfWork>();
            _context.Setup(x => x.CreateUnitOfWork()).Returns(_uow.Object);
        }

        [Fact]
        public async Task MemberWasCreated()
        {
            // Arrange
            var sut = new CreateMemberHandler(_context.Object, _memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _memberRepo.Verify(x => x.Add(It.IsAny<Member>()), Times.Once());
        }

        [Fact]
        public async Task MemberWasCreatedWithLoanerCard()
        {
            // Arrange
            var sut = new CreateMemberHandler(_context.Object, _memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _loanerCardRepo.Verify(x => x.Add(It.IsAny<LoanerCard>()), Times.Once());
        }

        [Fact]
        public void NoneUniqueSsn()
        {
            // Arrange
            var sut = new CreateMemberHandler(_context.Object, _memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);
            _memberRepo.Setup(x => x.GetBySsn(It.IsAny<string>())).Returns(new Member());

            // Act And Assert
            Assert.Throws<NotUniqueSsnException>(() => sut.Handle(_command, CancellationToken.None).Wait());
        }

        [Theory]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "", false)]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "", "5550", false)]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "", "Paris", "5550", false)]
        [InlineData("0123456789", "TestName", "test@test.dk", "", "12a", "Paris", "5550", false)]
        [InlineData("0123456789", "", "test@test.dk", "FakeStreet", "12a", "Paris", "5550", false)]
        [InlineData("", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550", false)]
        [InlineData("012345678", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550", false)] // boundary test length = 9
        [InlineData("01234567899", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550", false)] // boundary test length = 11
        [InlineData("0123456789", "TestName", "", "FakeStreet", "12a", "Paris", "5550", false)]
        [InlineData("0123456789", "TestName", "testest", "FakeStreet", "12a", "Paris", "5550", false)] // invalid email
        [InlineData("0123456789", "TestName", "@test.dk", "FakeStreet", "12a", "Paris", "5550", false)]  // invalid email
        [InlineData("0123456789", "TestName", "test@test.123", "FakeStreet", "12a", "Paris", "5550", false)] // invalid email
        [InlineData("0123456789", "TestName", "test@test.dk", "FakerStreetFakerStreetFakerStreetFakerStreetbvcfg4d", "12a", "Paris", "5550", false)] // too long streetname (51)
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "ParisParisParisParisParisParisx", "5550", false)] // too long city (31)
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "555055501", false)] // too long zip (9)
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12345678911", "Paris", "5550", false)] // too long housenumber (10)
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550", true)] // valid
        public void ValidatesCorrectly(string ssn, string name, string email, string streetName, string houseNumber, string city, string zipCode, bool expectedResult)
        {
            // Arrange
            var sut = new CreateMemberCommandValidator();

            _command.Ssn = ssn;
            _command.Name = name;
            _command.Email = email;
            _command.StreetName = streetName;
            _command.HouseNumber = houseNumber;
            _command.City = city;
            _command.ZipCode = zipCode;
            _command.AddressType = AddressType.HOME;

            // Act
            var validationRes = sut.Validate(_command);

            // Assert
            Assert.Equal(expectedResult, validationRes.IsValid);
        }

        [Fact]
        public async Task AddAddressWasCalled()
        {
            // Arrange
            var sut = new CreateMemberHandler(_context.Object, _memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _addressRepo.Verify(x => x.Add(It.IsAny<Address>()), Times.Once());
        }
    }
}
