using System.Data;
using FluentValidation;

namespace GTL.Application.UseCases.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
    {
        public CreateMaterialCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Edition).NotNull();
            
        }
    }
}