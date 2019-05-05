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

        public CustomCookieAuthenticationEvents(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            await Task.Delay(1);
            //var userPrincipal = context.Principal;

            //var changedClaim = userPrincipal.Claims.FirstOrDefault(x => x.Type == "Last Changed");
            //var idClaim = userPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //if (changedClaim == null || idClaim == null || !await _signInManager.ValidateLastChanged(Int32.Parse(idClaim.Value), changedClaim.Value))
            //{
            //    context.RejectPrincipal();


            //    await context.HttpContext.SignOutAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme);
            //}
        }
    }
}
