using MediatR;

namespace GTL.Application.UseCases.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommand : IRequest
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
    }
}