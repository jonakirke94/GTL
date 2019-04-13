using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Interfaces.Authentication.IdentityModels
{
    public class SignInResult
    {
        public User User { get; set; }
        public bool Success { get; set; }

        public SignInResult(User user = null, bool success = false)
        {
            User = user;
            Success = success;
        }
    }
}
