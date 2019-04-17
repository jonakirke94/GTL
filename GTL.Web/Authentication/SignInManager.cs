using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Authentication;

namespace GTL.Web.Authentication
{
    public class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _context;
        private readonly IUserRepository _userRepo;

        public SignInManager(IHttpContextAccessor context, IUserRepository userRepo, IAuthService authService)
        {
            _context = context;
            _userRepo = userRepo;
        }

        public int GetCurrentUserId()
        {
            if (!_context.HttpContext.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;
       
            int.TryParse(claim.Value, out int currentId);

            return currentId;
        }

        public bool IsSignedIn()
        {
            return _context.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task SignInAsync(ClaimsPrincipal principal, bool isPersistent)
        {   
             await _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                 principal,
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = isPersistent,
                AllowRefresh = false
            });
        }

        public async Task<bool> ValidateLastChanged(int id, string lastChanged)
        {
            var user = await _userRepo.GetUserByIdAsync(id, CancellationToken.None);

            if (user == null)
            {
                return false;
            }

            return user.LastChanged > DateTime.Parse(lastChanged);        
        }

        public async Task SignOutAsync()
        {
            await _context.HttpContext.SignOutAsync();
        }
    }
}
