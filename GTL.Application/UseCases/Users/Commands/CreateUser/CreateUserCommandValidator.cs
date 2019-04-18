using FluentValidation;

namespace GTL.Application.UseCases.Users.Commands.CreateUser
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

            RuleFor(x => x.PermissionLevel).NotNull();
        }

        //private static bool ValidDanishZip(CreateUserCommand model, string zipCode, PropertyValidatorContext ctx)
        //{
        //    return !model.ZipCode.StartsWith("0");
        //}
    }
 
}
