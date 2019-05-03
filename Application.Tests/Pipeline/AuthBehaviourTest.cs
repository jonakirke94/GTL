using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GTL.Application.Exceptions;
using GTL.Application.Helper.CustomAttributes;
using GTL.Application.Infrastructure.Pipeline;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
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
        public void ThrowsIfHasAttributeAndNoSsnFound()
        {
            // Arrange
            var request = new Mock<DummyRequestWithAttribute>();
            _currentUser.Setup(x => x.IsAuthenticated()).Returns(true);
            _currentUser.Setup(x => x.GetSsn()).Returns(string.Empty);

            var sut =
                new RequestAuthBehaviour<DummyRequestWithAttribute, DummyResponse>(_currentUser.Object, _staffRepo.Object,
                    _memoryCache.Object);

            // Act and Assert
            Assert.Throws<AuthenticationException>(() => sut.Handle(request.Object, default, _mockPipelineBehaviourDelegate.Object).Wait());
        }

        public class DummyRequest : IRequest<DummyResponse>
        {
        }

        [Authorize(Role.CHIEFLIBRARIAN)]
        public class DummyRequestWithAttribute : IRequest<DummyResponse>
        {
        }

        public class DummyResponse
        {
        }
    }
}

