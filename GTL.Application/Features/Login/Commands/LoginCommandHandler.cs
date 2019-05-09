using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace GTL.Application.Features.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly ISignInManager _signInManager;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMemoryCache _cache;
        private readonly IStaffRepository _staffRepo;


        public LoginCommandHandler(ISignInManager signInManager, IPasswordHelper passwordHelper, IMemoryCache cache, IStaffRepository staffRepo)
        {
            _signInManager = signInManager;
            _passwordHelper = passwordHelper;
            _cache = cache;
            _staffRepo = staffRepo;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var staffAccount = _staffRepo.GetByEmail(request.Email);

            if (staffAccount == null)
            {
                return false;
            }

            var isMatching =
                _passwordHelper.ValidatePassword(request.Password, staffAccount.PasswordSalt, staffAccount.PasswordHash);

            if (!isMatching)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, staffAccount.Name),
                new Claim(ClaimTypes.NameIdentifier, staffAccount.Id.ToString()),
                new Claim(ClaimTypes.Role, staffAccount.Role.ToString()),
            };

            var userIdentity = new ClaimsIdentity(claims, "Basic");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await _signInManager.SignInAsync(userPrincipal, false);

            _cache.Set(staffAccount.Id.ToString(), staffAccount.Role.ToString(), CacheHelper.CacheOptions());

            return true;
        }
    }
}
