using GTL.Application.Interfaces.Repositories;
using Moq;
using System.Threading.Tasks;
using GTL.Application.UseCases.Materials.Commands.CreateMaterial;
using Xunit;

namespace Application.Tests
{
    public class CreateMaterialTests
    {
        [Theory]
        [InlineData("0123456789", "Titel", "Beskrivelse", 1)]
        [InlineData("0123456789012", "Titel", "Beskrivelse", -1)]
        [InlineData("", "Titel", "Beskrivelse", 30000)]
        public async Task MaterialWasCreatedTest(string isbn, string title, string description, int edition)
        {
            Mock<IMaterialRepository> materialRepository = new Mock<IMaterialRepository>();

            Mock<CreateMaterialCommand> command = new Mock<CreateMaterialCommand>();
            command.Object.Isbn = isbn;
            command.Object.Title = title;
            command.Object.Description = description;
            command.Object.Edition = edition;
            var sut = new CreateMaterialHandler(materialRepository.Object);

        }
    }
}