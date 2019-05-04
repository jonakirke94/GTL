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
            RuleFor(x => x.Isbn).Must(ValidIsbn).WithMessage("The ISBN is not a valid ISBN number");
            RuleFor(x => x.Title).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Edition).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Type).IsInEnum();
        }

        private static bool ValidIsbn(MaterialBaseCommand model, string isbn, PropertyValidatorContext ctx)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.Isbn))
                {
                    ISBN.For(model.Isbn);
                }
            }
            catch (ISBNInvalidException)
            {
                return false;
            }

            return true;
        }
    }
}
