using GTL.Application.Interfaces.Authentication.IdentityModels;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Authentication
{
    public interface ISignInManager
    {
        Task SignInAsync(string email, string password, bool isPersistent);

        Task SignOutAsync();

        Task<bool> ValidateLoginAsync(ClaimsPrincipal principal);

        Task<bool> ValidateLastChanged(string lastChanged);

        string GetCurrentUserId();

        bool IsSignedIn(ClaimsPrincipal principal);
    }
}
