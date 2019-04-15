using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Authentication.IdentityModels;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Web.Authentication
{
    //public class UserManager : IUserManager
    //{
    //    private readonly IUserStore _userStore;

    //    public UserManager(IUserStore userStore)
    //    {
    //        _userStore = userStore;
    //    }


    //    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    //    {
    //        await _userStore.CreateAsync(user, CancellationToken.None);
    //    }

    //    public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    //    {
    //        return await _userStore.GetUserByIdAsync(id, cancellationToken);
    //    }

    //    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
    //    {
    //        return await _userStore.GetUsersAsync(cancellationToken);
    //    }

    //    public async Task<SignInResult> ValidatePasswordAsync(string email, string password)
    //    {
    //        var result = new SignInResult
    //        {
    //            Success = false
    //        };

    //        var user = await _userStore.GetUserByEmailAsync(email, CancellationToken.None);

    //        if (user == null)
    //        {
    //            return result;
    //        }

    //        if (Hasher.Validate(password, user.PasswordSalt, user.PasswordHash))
    //        {
    //            result.Success = true;
    //        }

    //        result.User = user;
    //        return result;
    //      }
    //}
}
