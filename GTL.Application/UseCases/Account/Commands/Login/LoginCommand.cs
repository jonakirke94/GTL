using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Authorization.Permissions;
using GTL.Application.Interfaces.Authentication.IdentityModels;
using MediatR;

namespace GTL.Application.UseCases.Account.Commands.Login
{
    public class LoginCommand : AssistantLibrarian<SignInResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
