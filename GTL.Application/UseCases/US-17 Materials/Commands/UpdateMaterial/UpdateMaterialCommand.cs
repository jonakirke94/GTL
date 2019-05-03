using GTL.Application.UseCases.US_17_Materials.Commands;
using MediatR;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class UpdateMaterialCommand : IRequest
    {
        public int Id { get; set; }
        public MaterialBaseCommand Material { get; set; }
    }
}