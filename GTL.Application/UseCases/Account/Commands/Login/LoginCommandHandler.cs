using GTL.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Authentication.IdentityModels;
using System.Security.Claims;

namespace GTL.Application.UseCases.Account.Commands.Login
{
    public class Handler : IRequestHandler<LoginCommand, SignInResult>
    {
        private readonly IUserRepository _userRepo;
        private readonly ISignInManager _signInManager;
        private readonly IAuthService _authService;

        public Handler(IUserRepository userRepo, ISignInManager signInManager, IAuthService authService)
        {
            _userRepo = userRepo;
            _signInManager = signInManager;
            _authService = authService;
        }

        public async Task<SignInResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ValidatePasswordAsync(request.Email, request.Password);

            if (!result.Success)
            {
                return result;
            }

            var roleClaims = new List<Claim>();

            foreach (var role in result.User.Roles)
            {
                new Claim(ClaimTypes.Role, role.NormalizedName);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.User.Name),
                new Claim(ClaimTypes.NameIdentifier, result.User.Id.ToString()),
                new Claim("Last Changed", result.User.LastChanged.ToLongDateString()),
            };

            claims.AddRange((roleClaims));

            var userIdentity = new ClaimsIdentity(claims, "Basic");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await _signInManager.SignInAsync(userPrincipal, request.IsPersistent);

            return result;
        }
    }
}
