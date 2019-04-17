using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.Users.Commands.UpdateUser;

namespace GTL.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

            // example of more complex validation with regex
            RuleFor(x => x.ZipCode).Matches(@"^\d{4}$")
                .Must(ValidDanishZip);

            RuleFor(x => x.City).NotEmpty();
        }

        private static bool ValidDanishZip(UpdateUserCommand model, string zipCode, PropertyValidatorContext ctx)
        {
            return !model.ZipCode.StartsWith("0");
        }
    }
}
