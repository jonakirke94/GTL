using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<IdentityModels.SignInResult> ValidatePasswordAsync(string email, string password);
    }
}
