using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Users;
using GTL.Application.UseCases.Users.Queries;
using GTL.Application.UseCases.Users.Queries.GetUserList;
using GTL.Application.ViewModels;
using GTL.Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class GetUserListTests
    {
        private readonly MockRepository _factory;
        private readonly Mock<IUserRepository> _userRepo;
        private readonly Mock<IMapper> _mapper;

        public GetUserListTests()
        {
           _factory = new MockRepository(MockBehavior.Loose);
           _userRepo = _factory.Create<IUserRepository>();
           _mapper = _factory.Create<IMapper>();        
        }

        [Fact]
        public async Task ReturnsCorrectViewModel()
        {
            // Arrange
            var query = _factory.Create<GetUserListQuery>();
            var viewModel = _factory.Create<UserListViewModel>();

            _userRepo.Setup(x => x.GetUsersAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<IEnumerable<User>>());
            _mapper.Setup(m => m.Map<UserListViewModel>(It.IsAny<IEnumerable<User>>())).Returns(viewModel.Object);
            var sut = new GetUserListQueryHandler(_userRepo.Object, _mapper.Object);

            // Act
            var result = await sut.Handle(query.Object, CancellationToken.None);

            // Assert
            var actual = result.GetType().BaseType;
            var expected = typeof(UserListViewModel);
            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task ReturnsCorrectUsers()
        {
            // Arrange
            var query = _factory.Create<GetUserListQuery>();
            var viewModel = _factory.Create<UserListViewModel>();

            var users = new List<UserDto>
            {
                new UserDto
                {
                    Id = 2,
                },
                new UserDto
                {
                    Id = 2,
                },
            };

            viewModel.Object.Users = users;

            _userRepo.Setup(x => x.GetUsersAsync(It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<IEnumerable<User>>());
            _mapper.Setup(m => m.Map<UserListViewModel>(It.IsAny<IEnumerable<User>>())).Returns(viewModel.Object);
            var sut = new GetUserListQueryHandler(_userRepo.Object, _mapper.Object);

            // Act
            var result = await sut.Handle(query.Object, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Users.Count());
        }
    }
}
