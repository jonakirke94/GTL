using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using MediatR;

namespace GTL.Application.UseCases.Materials.Commands.CreateMaterial
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
            _materialRepository.CreateMaterial(request.Isbn, request.Title, request.Description, request.Edition);

            return Task.Run(() => Unit.Value, cancellationToken);
        }

    }
}
