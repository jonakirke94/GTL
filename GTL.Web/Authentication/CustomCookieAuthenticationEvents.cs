using GTL.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GTL.Web.Authentication
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ISignInManager _signInManager;
        private readonly HttpContext _context;

        public CustomCookieAuthenticationEvents(ISignInManager signInManager, IHttpContextAccessor context)
        {
            _signInManager = signInManager;
            _context = context.HttpContext;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
                  
            var changedClaim = userPrincipal.Claims.FirstOrDefault(x => x.Type == "Last Changed");
            var idClaim = userPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        
            if (changedClaim == null || idClaim == null || !await _signInManager.ValidateLastChanged(Int32.Parse(idClaim.Value), changedClaim.Value))
            {
                context.RejectPrincipal();

                await _context.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
