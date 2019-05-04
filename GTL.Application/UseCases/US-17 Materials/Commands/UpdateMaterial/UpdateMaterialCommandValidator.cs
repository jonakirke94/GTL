using FluentValidation;

namespace GTL.Application.UseCases.Commands.UpdateMaterial
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
