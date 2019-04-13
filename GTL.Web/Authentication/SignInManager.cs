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
using System.Threading.Tasks;

namespace GTL.Web.Authentication
{
    public class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _context;

        public SignInManager(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _context.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task SignInAsync(User user, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            var userIdentity = new ClaimsIdentity(claims, "SecureLogin");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
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
            await _context.HttpContext.SignOutAsync();
        }

        public Task<bool> ValidateLoginAsync(ClaimsPrincipal principal)
        {
            return Task.Run(() => true);
        }
    }
}
