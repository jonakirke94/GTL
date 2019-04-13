using GTL.Application.Interfaces.Authentication;
using GTL.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Web.Authentication
{
    public class SignInManager : ISignInManager
    {
        private readonly HttpContext _context;
        private readonly IUserManager _userManager;

        public SignInManager(IHttpContextAccessor context, IUserManager userManager)
        {
            _context = context.HttpContext;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            if (!_context.User.Identity.IsAuthenticated)
                return string.Empty;

            Claim claim = _context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return string.Empty;
    
            return claim.Value;
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _context.User.Identity.IsAuthenticated;
        }

        public async Task SignInAsync(string email, string password, bool isPersistent)
        {
            // validate password
            var result = await _userManager.ValidatePasswordAsync(email, password);

            if (!result.Success)
            {
                // invalid login attempt
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.User.Name),
                new Claim(ClaimTypes.NameIdentifier, result.User.Id),
                new Claim("Last Changed", result.User.LastChanged.ToLongDateString()),
            };

            var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            userPrincipal,
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
        }

        public async Task SignOutAsync()
        {
            await _context.SignOutAsync();
        }

        public async Task<bool> ValidateLastChanged(string lastChanged)
        {
            var id = GetCurrentUserId();

            var user = await _userManager.GetUserByIdAsync(id, CancellationToken.None);

            return user.LastChanged > DateTime.Parse(lastChanged);        
        }

        public Task<bool> ValidateLoginAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}
