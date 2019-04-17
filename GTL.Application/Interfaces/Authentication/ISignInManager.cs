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
        Task SignInAsync(ClaimsPrincipal principal, bool isPersistent);

        Task SignOutAsync();

        Task<bool> ValidateLastChanged(int id, string lastChanged);

        int GetCurrentUserId();

        bool IsSignedIn();
    }
}
