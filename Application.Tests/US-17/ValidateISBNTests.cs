using GTL.Application.UseCases.Commands;
using GTL.Domain.Enums;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class ValidateISBNTests
    {
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

        [Theory]
        [InlineData("012345678")]
        [InlineData("01234567890")]
        [InlineData("012345678901")]
        [InlineData("0000000000001")]
        [InlineData("01234567890123")]
        public void CanValidateIncorrectIsbn(string isbn)
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
            Assert.False(validationRes.IsValid);
        }
    }
}
