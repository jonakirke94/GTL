using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Users.Commands.CreateUser;
using GTL.Domain.Entities;
using MediatR;
using Moq;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Application.Tests
{
    // for TASK objects .Returns(Task.FromResult(default(object)))
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
            var validation = _validator.Validate(_command.Object);
            var res = await handler.Handle(_command.Object, CancellationToken.None);           

            // Assert
            Assert.True(validation.IsValid);
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

        public void Dispose()
        {
            // teardown is xUnit
        }
    }
}
