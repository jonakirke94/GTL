using System;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Authentication;
using GTL.Application.Authorization;
using GTL.Application.Exceptions;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GTL.Application.Infrastructure.Pipeline
{
    public class RequestAuthorization<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : CommandResponse
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IAuthService _authService;

        public RequestAuthorization(IAuthService authService, ILogger<TRequest> logger, IUserRepository userRepo, ICurrentUser currentUser)
        {        
            _logger = logger;
            _userRepo = userRepo;
            _currentUser = currentUser;
            _authService = authService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var type = typeof(TRequest).BaseType.Name;

            if (type == "AuthCommand")
            {
                if (!_currentUser.IsAuthenticated())
                {
                    return await Error("User not authenticated");
                }

                var id = _currentUser.GetUserId();

                if (id == -1)
                {
                    throw new InvalidIdException();
                }

                var user = await _userRepo.GetUserByIdAsync(id, cancellationToken);

                var auth = request as AuthCommand;
                var isAuthorized = _authService.HasPermission(auth.PermissionLevel, user);


                if (!isAuthorized)
                {
                    return await Error("You do not have permissions to complete your request");
                }
            }

            var response = await next();
            return response;
        }

        private static Task<TResponse> Error(string errorMessage)
        {
            var response = Activator.CreateInstance<TResponse>();

            response.ErrorMessage = errorMessage;

            return Task.FromResult(response);
        }
    }
}
