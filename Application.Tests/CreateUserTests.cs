using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Users.Commands.CreateUser;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CreateUserTests : IDisposable
    {
        private readonly CreateUserCommandValidator _validator;
        private readonly Mock<CreateUserCommand> _command;

        public CreateUserTests()
        {
            _validator = new CreateUserCommandValidator();
            _command = new Mock<CreateUserCommand>();
        }

        
        [Theory]
        [InlineData("Thanos", "Thanos@gmail.com", "securepassword", PermissionLevel.CHIEFLIBRARIAN)]
        public async Task CreateUserSuccessfully(string name, string email, string password, PermissionLevel level)
        {
            // Arrange
            var userRepo = new Mock<IUserRepository>();
            _command.Object.Name = name;
            _command.Object.Email = email;
            _command.Object.Password = password;
            _command.Object.PermissionLevel = level;

            userRepo.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var handler = new CreateUserCommandHandler(userRepo.Object);

            // Act
            var res = await handler.Handle(_command.Object, CancellationToken.None);

            // Assert
            Assert.IsType<Unit>(res);
        }

        [Theory]
        [InlineData("T")]
        [InlineData(null)]
        public void ShouldHaveNameValidation(string name)
        {
            _command.Object.Name = name;
            _validator.ShouldHaveValidationErrorFor(user => user.Name, _command.Object);
        }

        [Theory]
        [InlineData("FakeEmail")]
        [InlineData("FakeEmail@")]
        [InlineData(",.@")]
        [InlineData("Fake@gmail")]
        [InlineData("Fake@gmail.")]
        [InlineData("Fake@.com")]
        [InlineData("Fake@gmail.123")]
        public void ShouldHaveEmailValidation(string email)
        {
            _command.Object.Email = email;
            _validator.ShouldHaveValidationErrorFor(user => user.Email, _command.Object);
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData(null)]
        public void ShouldHavePasswordValidation(string password)
        {
            _command.Object.Password = password;
            _validator.ShouldHaveValidationErrorFor(user => user.Password, _command.Object);
        }

        [Theory]
        [InlineData("12345678", "123")]
        [InlineData("12345678", null)]
        [InlineData("12345678", "")]
        [InlineData("abcdefgh", "abcdefghi")]
        public void ShouldHaveConfirmPasswordValidation(string password, string confirmPassword)
        {
            _command.Object.Password = password;
            _command.Object.ConfirmPassword = confirmPassword;
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, _command.Object);
        }

        [Fact]
        public void ShouldHavePermissionValidation()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.PermissionLevel, _command.Object);
        }

        public void Dispose()
        {
            // teardown is xUnit
        }
    }
}
