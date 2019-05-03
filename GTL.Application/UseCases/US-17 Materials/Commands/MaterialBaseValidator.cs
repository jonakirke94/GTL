using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using GTL.Application.UseCases.US_17_Materials.Commands;
using GTL.Domain.Entities;
using GTL.Domain.Exceptions;
using GTL.Domain.ValueObjects;

namespace GTL.Application.Validators
{
    public class MaterialBaseValidator : AbstractValidator<MaterialBaseCommand>
    {
        public MaterialBaseValidator()
        {
            RuleFor(x => x.Isbn).Must(ValidIsbn).WithMessage("The ISBN is not a valid ISBN number");
            RuleFor(x => x.Title).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Edition).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
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
