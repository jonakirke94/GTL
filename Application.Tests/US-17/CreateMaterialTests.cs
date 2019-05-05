using System.ComponentModel;
using System.Threading;
using GTL.Application.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Commands;
using GTL.Application.UseCases.Commands.CreateMaterial;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using GTL.Domain.ValueObjects;
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
            Mock<IGTLContext> context = new Mock<IGTLContext>();
            var uow = new Mock<IUnitOfWork>();
            context.Setup(x => x.CreateUnitOfWork()).Returns(uow.Object);
            Mock<IMaterialRepository> materialRepository = new Mock<IMaterialRepository>();
            Mock<MaterialBaseCommand> command = new Mock<MaterialBaseCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;
            var sut = new CreateMaterialHandler(context.Object, materialRepository.Object);

            //// Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            materialRepository.Verify(
                x => x.Add(It.IsAny<Material>()), Times.Exactly(1));
        }

        [Theory]
        [ClassData(typeof(TDS5_EC2))]
        public void MaterialHasInvalidAttributesTest(string isbn, string title, string description, int edition)
        {
            // Arrange
            var sut = new MaterialBaseValidator();

            Mock<MaterialBaseCommand> command = new Mock<MaterialBaseCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;

            // Act
            var validationRes = sut.Validate(command.Object);

            // Assert
            Assert.False(validationRes.IsValid);
        }

        [Theory]
        [InlineData("80-902734-1-6")]
        [InlineData("1-84356-028-3")]
        [InlineData("9780545935173")]
        [InlineData("9780395489321")]
        public void CanValidateCorrectIsbn(string isbn)
        {
            // Arrange
            var sut = new MaterialBaseValidator();

            var command = new Mock<MaterialBaseCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = "FakeTitle";
            command.Object.Description = "FakeDescription";
            command.Object.Edition = 1;
            command.Object.Type = MaterialType.Book;

            // Act
            var validationRes = sut.Validate(command.Object);

            // Assert
            Assert.True(validationRes.IsValid);
        }
    }
}