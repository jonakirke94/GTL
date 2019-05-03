using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Application.UseCases.Commands.CreateMaterial;
using GTL.Domain.Entities;
using GTL.Domain.ValueObjects;
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
            var material = new Material
            {
                Id = request.Id,
                ISBN = ISBN.For(request.Material.Isbn),
                Title = request.Material.Title,
                Description = request.Material.Description,
                Edition = request.Material.Edition
            };

            using (var db = _context.CreateUnitOfWork())
            {

                // hvorfor passer du ikke objeket du lige har lavet i stedet?
                //_materialRepository.Update(material);


                //_materialRepository.Update(request.Isbn, request.Title, request.Description, request.Edition);

                db.SaveChanges();
            }

            return Task.Run(() => Unit.Value, cancellationToken);
            // TODO: Implement this
        }
    }
}
