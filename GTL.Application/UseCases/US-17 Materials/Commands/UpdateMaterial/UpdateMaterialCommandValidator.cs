using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
    {
        public UpdateMaterialCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Isbn).Must(length => length.Length == 0 || length.Length == 10 || length.Length == 13);
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(60);
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Edition).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
        }

    }
}
