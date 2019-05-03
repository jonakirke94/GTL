using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Commands.CreateMaterial;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Commands.WriteMaterial
{
    public class UpdateMaterialHandler
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IGTLContext _context;

        public UpdateMaterialHandler(IGTLContext context, IMaterialRepository materialRepository)
        {
            _context = context;
            _materialRepository = materialRepository;
        }

        public Task<Unit> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            using (var db = _context.CreateUnitOfWork())
            {
                var isbn = new Domain.ValueObjects.ISBN
                {
                    Number = request.Isbn
                };

                Material material = new Material
                {
                    Id = request.Id,
                    ISBN = isbn,
                    Title = request.Title,
                    Description = request.Description,
                    Edition = request.Edition
                };
                _materialRepository.Update(request.Isbn, request.Title, request.Description, request.Edition);

                db.SaveChanges();
            }

            return Task.Run(() => Unit.Value, cancellationToken);
            // TODO: Implement this
        }
    }
}
