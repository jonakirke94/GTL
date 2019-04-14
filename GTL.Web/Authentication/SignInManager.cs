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

        public int GetCurrentUserId()
        {
            if (!_context.User.Identity.IsAuthenticated)
                return -1;

            Claim claim = _context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;
       
            int.TryParse(claim.Value, out int currentId);

            return currentId;
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _context.User.Identity.IsAuthenticated;
        }

        public async Task SignInAsync(string email, string password, bool isPersistent)
        {
            await _context.SignOutAsync();

            // validate password
            var result = await _userManager.ValidatePasswordAsync(email, password);

            if (!result.Success)
            {
                return;
                // invalid login attempt
            }

            // vores User skal have en liste af sine roller ogsåm så vi kan tilføje de rigtige claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.User.Name),
                new Claim(ClaimTypes.NameIdentifier, result.User.Id.ToString()),
                new Claim("Last Changed", result.User.LastChanged.ToLongDateString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Member"),
            };

            var userIdentity = new ClaimsIdentity(claims, "Basic");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

             await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            userPrincipal,
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = isPersistent,
                AllowRefresh = false
            });
        }

        public async Task<bool> ValidateLastChanged(int id, string lastChanged)
        {
            var user = await _userManager.GetUserByIdAsync(id, CancellationToken.None);

            if (user == null)
            {
                return false;
            }

            return user.LastChanged > DateTime.Parse(lastChanged);        
        }

        public async Task SignOutAsync()
        {
            await _context.SignOutAsync();
        }

        public Task<bool> ValidateLoginAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}
