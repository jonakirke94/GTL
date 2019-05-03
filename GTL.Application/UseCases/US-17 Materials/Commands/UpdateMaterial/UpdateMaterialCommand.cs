using MediatR;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class UpdateMaterialCommand : IRequest
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
    }
}