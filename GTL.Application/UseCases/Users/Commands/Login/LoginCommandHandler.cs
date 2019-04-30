using GTL.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using System.Security.Claims;
using GTL.Application.Helper;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.UseCases.Users.Commands.Login;
using GTL.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace GTL.Application.UseCases.Account.Commands.Login
{
    public class Handler : IRequestHandler<LoginCommand, SignInResult>
    {
        private readonly ISignInManager _signInManager;
        private readonly IAuthService _authService;
        private readonly IMemoryCache _cache;

        public Handler(ISignInManager signInManager, IAuthService authService, IMemoryCache cache)
        {
            _signInManager = signInManager;
            _authService = authService;
            _cache = cache;
        }

        public async Task<SignInResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ValidatePasswordAsync(request.Email, request.Password);

            if (!result.SuccessfulLogin)
            {
                return result;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.User.Name),
                new Claim(ClaimTypes.NameIdentifier, result.User.Id.ToString()),
                new Claim(ClaimTypes.Role, result.User.PermissionLevel.ToString()),
            new Claim("Last Changed", result.User.LastChanged.ToLongDateString()),
            };

            var userIdentity = new ClaimsIdentity(claims, "Basic");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await _signInManager.SignInAsync(userPrincipal, request.IsPersistent);

            // add user permission to memory cache              
            _cache.Set(result.User.Id.ToString(), result.User.PermissionLevel, CacheHelper.CacheOptions());

            return result;
        }
    }
}
