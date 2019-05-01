using FluentValidation;
using FluentValidation.Validators;

namespace GTL.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.PermissionLevel).NotEmpty().IsInEnum();
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
        }
    }
 
}
