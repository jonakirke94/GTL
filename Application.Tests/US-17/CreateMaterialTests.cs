using System.ComponentModel;
using System.Threading;
using GTL.Application.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using Application.Tests.US_17;
using GTL.Application.UseCases.Commands.CreateMaterial;
using GTL.Application.UseCases.Materials.Commands.CreateMaterial;
using Xunit;

namespace Application.Tests
{
    public class CreateMaterialTests
    {
        [Theory]
        [ClassData(typeof(TDS5_EC1))]
        public async Task MaterialWasCreatedTest(string isbn, string title, string description, int edition)
        {
            // Arrange
            Mock<IMaterialRepository> materialRepository = new Mock<IMaterialRepository>();
            Mock<CreateMaterialCommand> command = new Mock<CreateMaterialCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;
            var sut = new CreateMaterialHandler(materialRepository.Object);

            // Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            materialRepository.Verify(
                x => x.CreateMaterial(isbn, title, description, edition), Times.Once);
        }

        [Theory]
        [ClassData(typeof(TDS5_EC2))]
        public void MaterialHasInvalidAttributesTest(string isbn, string title, string description, int edition)
        {
            // Arrange
            var sut = new CreateMaterialCommandValidator();

            Mock<CreateMaterialCommand> command = new Mock<CreateMaterialCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;

            // Act
            var validationRes = sut.Validate(command.Object);

            // Assert
            Assert.False(validationRes.IsValid);
        }


        //public void ShouldThrowValidationError(string ssn, string name, string email, string streetName, string houseNumber, string city, string zipCode)
        //{
        //    // Arrange
        //    var sut = new CreateMemberCommandValidator();

        //    _command.Ssn = ssn;
        //    _command.Name = name;
        //    _command.Email = email;
        //    _address.StreetName = streetName;
        //    _address.HouseNumber = houseNumber;
        //    _address.City = city;
        //    _address.ZipCode = zipCode;
        //    _address.AddressType = AddressType.HOME;

        //    _command.Address = _address;

        //    // Act
        //    var validationRes = sut.Validate(_command);

        //    // Assert
        //    Assert.False(validationRes.IsValid);
        //}

    }
}