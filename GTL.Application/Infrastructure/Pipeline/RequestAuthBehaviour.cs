using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Users.Commands.DeleteUser;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.Infrastructure.Pipeline
{
    public class RequestAuthBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserRepository _userRepo;
        private readonly IPermissionFactory _permissionFactory;

        public RequestAuthBehaviour(IPermissionFactory permissionFactory, ICurrentUser currentUser, IUserRepository userRepo)
        {
            _currentUser = currentUser;
            _userRepo = userRepo;
            _permissionFactory = permissionFactory;
        }

        // this authenticates all requests. It checks if the command or query implements an interface like IAdminRequest and gets the associated permission level
        // afterwards it fetches the current user from the ICurrentUser interfaces and IUserRepo and validates the user has the correct permission
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var required = _permissionFactory.GetPermissionByRequest<TRequest, TResponse>(request);

            if (required == PermissionLevel.DEFAULT)            
                return next();
                    
            var currentUserPermission = PermissionLevel.DEFAULT;

            try
            {
                var id = _currentUser.GetUserId();
                var user = _userRepo.GetUserByIdAsync(id, cancellationToken);
                currentUserPermission = user.Result.PermissionLevel;
            }
            catch (Exception e)
            {
                // throw database exception or more specific exception
            }

            if (currentUserPermission < required)
            {
                throw new AuthException(required);
            }


            return next();                           
        }
    }
}
