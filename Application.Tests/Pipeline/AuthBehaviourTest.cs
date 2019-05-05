using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GTL.Application.Exceptions;
using GTL.Application.Helper.CustomAttributes;
using GTL.Application.Infrastructure.Pipeline;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace Application.Tests.Pipeline
{
    public class AuthBehaviourTest
    {
        private readonly Mock<IStaffRepository> _staffRepo;
        private readonly Mock<IMemoryCache> _memoryCache;
        private readonly Mock<ICurrentUser> _currentUser;
        private readonly Mock<RequestHandlerDelegate<DummyResponse>> _mockPipelineBehaviourDelegate;
        public AuthBehaviourTest()
        {
            _staffRepo = new Mock<IStaffRepository>();
            _memoryCache = new Mock<IMemoryCache>();
            _currentUser = new Mock<ICurrentUser>();
            _mockPipelineBehaviourDelegate = new Mock<RequestHandlerDelegate<DummyResponse>>();
        }

        [Fact]
        public void PassesIfNoAttribute()
        {
            // Arrange
            var request = new Mock<DummyRequest>();

            var sut =
                new RequestAuthBehaviour<DummyRequest, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act
            sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object);

            // Assert
            _mockPipelineBehaviourDelegate.Verify(x => x(), Times.Once);
        }

        [Fact]
        public void ThrowsIfHasAttributeAndNotAuthenticated()
        {
            // Arrange
            var request = new Mock<DummyRequestWithAttribute>();
            _currentUser.Setup(x => x.IsAuthenticated()).Returns(false);

            var sut =
                new RequestAuthBehaviour<DummyRequestWithAttribute, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act and Assert
            Assert.Throws<AuthenticationException>(() => sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object).Wait());
        }

        [Fact]
        public void ThrowsIfHasAttributeAndNoIdFound()
        {
            // Arrange
            var request = new Mock<DummyRequestWithAttribute>();
            _currentUser.Setup(x => x.IsAuthenticated()).Returns(true);
            _currentUser.Setup(x => x.GetId()).Returns(-1);

            var sut =
                new RequestAuthBehaviour<DummyRequestWithAttribute, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act and Assert
            Assert.Throws<AuthenticationException>(() => sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object).Wait());
        }

        [Theory]
        [InlineData(Role.ASSOCIATELIBRARIAN)] // 1 higher permission
        [InlineData(Role.REFERENCELIBRARIAN)] // same permission as required
        public void PassesWithValidPermission(Role staffRole)
        {
            // Arrange
            var request = new Mock<DummyRequestWithAttribute>();
            _currentUser.Setup(x => x.IsAuthenticated()).Returns(true);
            _currentUser.Setup(x => x.GetId()).Returns(123);

            object fakeRole = null;
            _memoryCache.Setup(x => x.TryGetValue(It.IsAny<object>(), out fakeRole)).Returns(false);

            var cacheEntry = Mock.Of<ICacheEntry>();

            _memoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cacheEntry);

            var fakeStaff = new Mock<Staff>().Object;
            fakeStaff.Role = staffRole;

            _staffRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(fakeStaff);

            var sut =
                new RequestAuthBehaviour<DummyRequestWithAttribute, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act
            sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object);

            // Assert
            _mockPipelineBehaviourDelegate.Verify(x => x(), Times.Once);
        }

        [Fact]
        public void ThrowsAuthExceptionOnTooLowPermission()
        {
            // Arrange
            var request = new Mock<DummyRequestWithAttribute>();
            _currentUser.Setup(x => x.IsAuthenticated()).Returns(true);
            _currentUser.Setup(x => x.GetId()).Returns(123);

            object fakeRole = null;
            _memoryCache.Setup(x => x.TryGetValue(It.IsAny<object>(), out fakeRole)).Returns(false);

            var cacheEntry = Mock.Of<ICacheEntry>();

            _memoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cacheEntry);

            var fakeStaff = new Mock<Staff>().Object;
            fakeStaff.Role = Role.CHECKOUTSTAFF;

            _staffRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(fakeStaff);

            var sut =
                new RequestAuthBehaviour<DummyRequestWithAttribute, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act and Assert
            Assert.Throws<AuthorizeException>(() => sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object).Wait());
        }

        public class DummyRequest : IRequest<DummyResponse>
        {
        }

        [Authorize(Role.REFERENCELIBRARIAN)]
        public class DummyRequestWithAttribute : IRequest<DummyResponse>
        {
        }

        public class DummyResponse
        {
        }
    }
}

