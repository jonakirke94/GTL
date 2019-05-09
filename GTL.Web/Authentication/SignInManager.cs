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

namespace GTL.Web.Authentication
{
    public class SignInManager : ISignInManager
    {
        private readonly IHttpContextAccessor _context;

        public SignInManager(IHttpContextAccessor context)
        {
            _context = context;
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
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                IsPersistent = isPersistent,
                AllowRefresh = false
            });
        }

        public async Task<bool> ValidateLastChanged(int id, string lastChanged)
        {
            //var user = await _userRepo.GetUserByIdAsync(id, CancellationToken.None);

            //if (user == null)
            //{
            //    return false;
            //}

            //return user.LastChanged > DateTime.Parse(lastChanged);        
            return true;
        }

        public async Task SignOutAsync()
        {
            await _context.HttpContext.SignOutAsync();
        }
    }
}
