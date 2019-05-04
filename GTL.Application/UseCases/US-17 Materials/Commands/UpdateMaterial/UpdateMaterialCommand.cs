using MediatR;

namespace GTL.Application.UseCases.Commands.UpdateMaterial
{
    public class UpdateMaterialCommand : IRequest
    {
        public int Id { get; set; }
        public MaterialBaseCommand Material { get; set; }
    }
}