using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Login.Commands;
using GTL.Application.UseCases.Members.Commands.CreateMember;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class LoginTests
    {
        private readonly LoginCommand _command;
        private readonly Mock<IStaffRepository> _staffRepo;
        private readonly Mock<ISignInManager> _signInManager;
        private readonly Mock<IPasswordHelper> _passwordHelper;
        private readonly Mock<IMemoryCache> _memoryCache;


        public LoginTests()
        {
            _command = Mock.Of<LoginCommand>();
            _staffRepo = new Mock<IStaffRepository>();
            _signInManager = new Mock<ISignInManager>();
            _passwordHelper = new Mock<IPasswordHelper>();
            _memoryCache = new Mock<IMemoryCache>();
        }

        [Fact]
        public async Task SignIntoHttpContextCalled()
        {
            // Arrange
            _command.Email = "test@test.dk";
            _command.Password = "secretpassword";

            var fakeStaff = Mock.Of<Staff>();
            fakeStaff.Id = 123;
            fakeStaff.Name = "FakeName";
            fakeStaff.Role = Role.LIBRARYASSISTANT;

            _staffRepo.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(fakeStaff);
            _passwordHelper.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _signInManager.Setup(x => x.SignInAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<bool>())).Returns(Task.FromResult(0));

            var cacheEntry = Mock.Of<ICacheEntry>();

            _memoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cacheEntry);

            var sut = new LoginCommandHandler(_signInManager.Object, _passwordHelper.Object, _memoryCache.Object, _staffRepo.Object);

            // Act
            await sut.Handle(_command, default);

            // Assert
            _signInManager.Verify(x => x.SignInAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task SuccessfulLogin()
        {
            // Arrange
            _command.Email = "test@test.dk";
            _command.Password = "secretpassword";

            var fakeStaff = Mock.Of<Staff>();
            fakeStaff.Id = 123;
            fakeStaff.Name = "FakeName";
            fakeStaff.Role = Role.LIBRARYASSISTANT;

            _staffRepo.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(fakeStaff);
            _passwordHelper.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _signInManager.Setup(x => x.SignInAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<bool>())).Returns(Task.FromResult(0));

            var cacheEntry = Mock.Of<ICacheEntry>();

            _memoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cacheEntry);

            var sut = new LoginCommandHandler(_signInManager.Object, _passwordHelper.Object, _memoryCache.Object, _staffRepo.Object);

            // Act
            var result = await sut.Handle(_command, default);

            // Assert
            Assert.True(result);
        }



        [Theory]
        [InlineData("test@test", "123", false)]
        [InlineData("testest", "123", false)]
        [InlineData("@test.dk", "123", false)]
        [InlineData("test@test.1", "123", false)]
        [InlineData("23", "123", false)]
        [InlineData("", "123", false)]
        [InlineData("test@test.dk", "", false)]
        [InlineData("test@test.dk", "123", true)]

        public void CanValidateInputData(string email, string password, bool expectedResult)
        {
            // Arrange
            var sut = new LoginCommandValidator();

            _command.Email = email;
            _command.Password = password;

            // Act
            var validationRes = sut.Validate(_command);

            // Assert
            Assert.Equal(expectedResult, validationRes.IsValid);
        }

        [Fact]
        public async Task RejectsOnNOnExistingEmail() {
            _command.Email = "test@test.dk";
            _command.Password = "12345678";

            var sut = new LoginCommandHandler(_signInManager.Object, _passwordHelper.Object, _memoryCache.Object, _staffRepo.Object);

            var result = await sut.Handle(_command, default);

            Assert.False(result);
        }


        [Fact]
        public async Task RejectsOnWrongPassword()
        {
            _command.Email = "test@test.dk";
            _command.Password = "12345678";

            _staffRepo.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(new Staff());
            _passwordHelper.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
                
            var sut = new LoginCommandHandler(_signInManager.Object, _passwordHelper.Object, _memoryCache.Object, _staffRepo.Object);

            var result = await sut.Handle(_command, default);

            Assert.False(result);
        }


        [Fact]
        public void CanValidateUntamperedHash()
        {
            // Arrange  
            const string message = "SecretPassword";
            var salt = Hasher.CreateSalt();
            var hash = Hasher.Hash(message, salt);
            
            // Act  
            var match = Hasher.Validate(message, salt, hash);

            // Assert  
            Assert.True(match);
        }

        [Fact]
        public void CanInvalidateTamperedHash()
        {
            // Arrange  
            const string message = "SecretHash";
            var salt = Hasher.CreateSalt();
            const string hash = "SecretHash1";

            // Act  
            var match = Hasher.Validate(message, salt, hash);

            // Assert  
            Assert.False(match);
        }


    }
}
