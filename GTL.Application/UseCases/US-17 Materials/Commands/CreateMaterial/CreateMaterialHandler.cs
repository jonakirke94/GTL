using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.UseCases.Materials.Commands.CreateMaterial;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class CreateMaterialHandler
    {
        private readonly IMaterialRepository _materialRepository;

        public CreateMaterialHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public Task<Unit> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            Material existingMaterial = null;
            if (!string.IsNullOrWhiteSpace(request.Isbn))
            {
                existingMaterial = _materialRepository.GetMaterialByIsbn(request.Isbn);
            }

            if (existingMaterial == null)
            {
                _materialRepository.CreateMaterial(request.Isbn, request.Title, request.Description, request.Edition);
            }

            return Task.Run(() => Unit.Value, cancellationToken);
        }

    }
}
