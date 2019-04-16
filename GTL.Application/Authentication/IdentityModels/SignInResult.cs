using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Authorization;

namespace GTL.Application.Interfaces.Authentication.IdentityModels
{
    public class SignInResult : CommandResponse
    {
        public User User { get; set; }
        public bool SuccessfulLogin { get; set; }
    }
}
