using System.ComponentModel;
using System.Threading;
using GTL.Application.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using Application.Tests.US_17;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Commands.CreateMaterial;
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
            Mock<IMaterialRepository> materialRepository = new Mock<IMaterialRepository>();
            Mock<UpdateMaterialCommand> command = new Mock<UpdateMaterialCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;
            var sut = new CreateMaterialHandler(context.Object, materialRepository.Object);

            // Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            materialRepository.Verify(
                x => x.Add(isbn, title, description, edition), Times.Exactly(1));
        }

        [Theory]
        [ClassData(typeof(TDS5_EC2))]
        public void MaterialHasInvalidAttributesTest(string isbn, string title, string description, int edition)
        {
            // Arrange
            var sut = new UpdateMaterialCommandValidator();

            Mock<UpdateMaterialCommand> command = new Mock<UpdateMaterialCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;

            // Act
            var validationRes = sut.Validate(command.Object);

            // Assert
            Assert.False(validationRes.IsValid);
        }

    }
}