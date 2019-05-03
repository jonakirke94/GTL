using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using GTL.Application.Validators;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
    {
        public UpdateMaterialCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Material).SetValidator(new MaterialBaseValidator());
        }

    }
}
