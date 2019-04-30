using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Authorization;
using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;

namespace GTL.Application
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUser _currentUser;

        public AuthService(IUserRepository userRepo, ICurrentUser currentUser)
        {
            _userRepo = userRepo;
            _currentUser = currentUser;
        }

        public async Task<SignInResult> ValidatePasswordAsync(string email, string password)
        {
            var result = new SignInResult
            {
                SuccessfulLogin = false
            };

            var user = await _userRepo.GetUserByEmailAsync(email, CancellationToken.None);

            if (user == null)
            {
                return result;
            }

            if (Hasher.Validate(password, user.PasswordSalt, user.PasswordHash))
            {
                result.SuccessfulLogin = true;
            }

            result.User = user;
            return result;
        }
    }
}
