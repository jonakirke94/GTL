using GTL.Application.Helper.CustomAttributes;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.UseCases.Commands
{
    // TODO Uncomment line below to enable auth
    //[Authorize(Role.ASSOCIATELIBRARIAN)]
    public class MaterialBaseCommand : IRequest
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
        public MaterialType Type { get; set; }
        public string Area { get; set; }
        public string Size { get; set; }
    }
}
