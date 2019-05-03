﻿using FluentValidation;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
    {

        public CreateMaterialCommandValidator()
        {
            RuleFor(x => x.Isbn).Must(length => length.Length == 0 || length.Length == 10 || length.Length == 13);
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(60);
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Edition).GreaterThanOrEqualTo(int.MinValue).LessThanOrEqualTo(int.MaxValue);
        }


    }
}
