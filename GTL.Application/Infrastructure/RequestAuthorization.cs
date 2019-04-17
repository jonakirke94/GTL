using System;
using GTL.Application.Authentication;
using GTL.Application.Authorization.Permissions;
using GTL.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Authorization;
using GTL.Application.Interfaces.Authentication;

namespace GTL.Application.Infrastructure
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

            if (type != "Object")
            {
                if (!_currentUser.IsAuthenticated())
                {
                    return await Error("User not authenticated!!!");
                }

                var id = _currentUser.GetUserId();

                if (id == -1)
                {
                    return await Error("User could not find user id");
                }


                var user = await _userRepo.GetUserByIdAsync(id, cancellationToken);

                if (type == "AuthCommand")
                {
                    var auth = request as AuthCommand;
                    var hasPermission = _authService.HasPermission(auth.PermissionLevel, user);

                }

                // may need to sanitize the name because the CLR appens a number to handle generic parameters
                var sanitizedType = type.Substring(0, type.LastIndexOf("`"));
                var isAuthorized = false;
                switch (sanitizedType)
                {
                    case "AssistantLibrarian":
                        var auth = request as AssistantLibrarian;
                        isAuthorized = await auth.Evaluate(user, cancellationToken);
                        break;
                    default:
                        // if no request has been hit we most likely forgot to add the authorizer to our command - throw exception
                        break;
                }

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
