using FluentValidation;
using FluentValidation.Validators;
using GTL.Domain.Exceptions;
using GTL.Domain.ValueObjects;

namespace GTL.Application.UseCases.Commands
{
    public class MaterialBaseValidator : AbstractValidator<MaterialBaseCommand>
    {
        public MaterialBaseValidator()
        {
            RuleFor(x => x.Isbn).NotEmpty().Must(ValidIsbn).WithMessage("The ISBN is not a valid ISBN number");
            RuleFor(x => x.Title).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Edition).GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Type).IsInEnum();
        }

        private static bool ValidIsbn(MaterialBaseCommand model, string isbn, PropertyValidatorContext ctx)
        {
            try
            {
                ISBN.For(model.Isbn);
            }
            catch (ISBNInvalidException)
            {
                return false;
            }

            return true;
        }
    }
}
