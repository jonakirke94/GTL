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

namespace Application.Tests
{
    public class CreateMemberTest
    {
        private readonly Mock<IMemberRepository> _memberRepo;
        private readonly Mock<ILoanerCardRepository> _loanerCardRepo;
        private readonly Mock<IAddressRepository> _addressRepo;
        private readonly CreateMemberCommand _command;
        private readonly Address _address;

        public CreateMemberTest()
        {
            _memberRepo = new Mock<IMemberRepository>();
            _loanerCardRepo = new Mock<ILoanerCardRepository>();
            _addressRepo = new Mock<IAddressRepository>();
            _command = new Mock<CreateMemberCommand>().Object;
            _address = new Mock<Address>().Object;
        }


        [Fact]
        public async Task MemberWasCreated()
        {
            // Arrange
            var sut = new CreateMemberHandler(_memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _memberRepo.Verify(x => x.CreateMember(It.IsAny<Member>()), Times.Once());
        }

        [Fact]
        public async Task MemberWasCreatedWithLoanerCard()
        {
            // Arrange
            var sut = new CreateMemberHandler(_memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _loanerCardRepo.Verify(x => x.CreateLoanerCard(It.IsAny<LoanerCard>()), Times.Once());
        }

        [Fact]
        public void NoneUniqueSsn()
        {
            // Arrange
            var sut = new CreateMemberHandler(_memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);
            _memberRepo.Setup(x => x.GetMemberBySsn(It.IsAny<string>())).Returns(new Member());

            // Act And Assert
            Assert.Throws<NotUniqueSsnException>(() => sut.Handle(_command, CancellationToken.None).Wait());
        }

        [Theory]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "")]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "12a", "", "5550")]
        [InlineData("0123456789", "TestName", "test@test.dk", "FakeStreet", "", "Paris", "5550")]
        [InlineData("0123456789", "TestName", "test@test.dk", "", "12a", "Paris", "5550")]
        [InlineData("0123456789", "", "test@test.dk", "FakeStreet", "12a", "Paris", "5550")]
        [InlineData("", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550")]
        [InlineData("012345678", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550")] // boundary test length = 9
        [InlineData("01234567899", "TestName", "test@test.dk", "FakeStreet", "12a", "Paris", "5550")] // boundary test length = 11
        [InlineData("0123456789", "TestName", "", "FakeStreet", "12a", "Paris", "5550")]
        [InlineData("0123456789", "TestName", "testest", "FakeStreet", "12a", "Paris", "5550")] // invalid email
        [InlineData("0123456789", "TestName", "@test.dk", "FakeStreet", "12a", "Paris", "5550")]  // invalid email
        [InlineData("0123456789", "TestName", "test@test.123", "FakeStreet", "12a", "Paris", "5550")] // invalid email
        public void ShouldThrowValidationError(string ssn, string name, string email, string streetName, string houseNumber, string city, string zipCode)
        {
            // Arrange
            var sut = new CreateMemberCommandValidator();

            _command.Ssn = ssn;
            _command.Name = name;
            _command.Email = email;
            _address.StreetName = streetName;
            _address.HouseNumber = houseNumber;
            _address.City = city;
            _address.ZipCode = zipCode;
            _address.AddressType = AddressType.HOME;
            
            _command.Addresses.Add(_address);

            // Act
            var validationRes = sut.Validate(_command);
        
            // Assert
            Assert.False(validationRes.IsValid);
        }

        [Fact]
        public void ShouldHaveAtleastOneAddress()
        {
            // Arrange
            var sut = new CreateMemberCommandValidator();

            _command.Ssn = "0123456789";
            _command.Name = "Fake Name";
            _command.Email = "test@test.dk";

            // Act
            var validationRes = sut.Validate(_command);

            // Assert
            Assert.Equal("A new member must have at least one address", validationRes.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task CanAddTwoAddresses()
        {
            // Arrange
            var sut = new CreateMemberHandler(_memberRepo.Object, _loanerCardRepo.Object, _addressRepo.Object);

            var address2 = new Mock<Address>().Object;

            _command.Ssn = "0123456789";
            _command.Name = "Fake Name";
            _command.Email = "test@test.dk";
            _address.StreetName = "test street";
            _address.HouseNumber = "12a";
            _address.City = "Aalborg";
            _address.ZipCode = "5550";
            _address.AddressType = AddressType.HOME;
            address2.StreetName = "fake street";
            address2.HouseNumber = "127";
            address2.City = "Aarhus";
            address2.ZipCode = "7000";
            address2.AddressType = AddressType.CAMPUS;

            _command.Addresses.Add(_address);
            _command.Addresses.Add(address2);

            // Act
            await sut.Handle(_command, CancellationToken.None);

            // Assert
            _addressRepo.Verify(x => x.AddAddress(It.IsAny<Address>()), Times.Exactly(2));
        }
    }
}
