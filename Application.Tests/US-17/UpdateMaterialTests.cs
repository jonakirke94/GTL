using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Commands;
using GTL.Application.UseCases.Commands.CreateMaterial;
using GTL.Application.UseCases.Commands.UpdateMaterial;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class UpdateMaterialTests
    {

        [Theory]
        [ClassData(typeof(TDS5_EC1))]
        public async Task EditMaterialShouldBePossibleTest(string isbn, string title, string description, int edition, MaterialType type)
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
            command.Object.Type = type;
            var sut = new UpdateMaterialHandler(context.Object, materialRepository.Object);

            //// Act
            await sut.Handle(command.Object, CancellationToken.None);

            // Assert
            materialRepository.Verify(
                x => x.Update(It.IsAny<Material>()), Times.Exactly(1));
        }

    }
}
