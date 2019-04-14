using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

            // example of more complex validation with regex
            //RuleFor(x => x.ZipCode).Matches(@"^\d{4}$")
            //    .Must(ValidDanishZip);

            RuleFor(x => x.Password).NotEmpty();
        }

        //private static bool ValidDanishZip(CreateUserCommand model, string zipCode, PropertyValidatorContext ctx)
        //{
        //    return !model.ZipCode.StartsWith("0");
        //}
    }
 
}
