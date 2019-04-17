using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.Users.Commands.Login;

namespace GTL.Application.UseCases.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
