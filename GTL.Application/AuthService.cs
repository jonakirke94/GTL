﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Authentication.IdentityModels;
using GTL.Application.Interfaces.Repositories;

namespace GTL.Application
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<SignInResult> ValidatePasswordAsync(string email, string password)
        {
            var result = new SignInResult
            {
                Success = false
            };

            var user = await _userRepo.GetUserByEmailAsync(email, CancellationToken.None);

            if (user == null)
            {
                return result;
            }

            if (Hasher.Validate(password, user.PasswordSalt, user.PasswordHash))
            {
                result.Success = true;
            }

            result.User = user;
            return result;
        }
    }
}
