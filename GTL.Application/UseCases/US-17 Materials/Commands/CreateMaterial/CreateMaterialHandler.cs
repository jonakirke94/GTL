using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class CreateMaterialHandler
    {
        private readonly IGTLContext _context;
        private readonly IMaterialRepository _materialRepository;

        public CreateMaterialHandler(IGTLContext context, IMaterialRepository materialRepository)
        {
            _context = context;
            _materialRepository = materialRepository;
        }

        public Task<Unit> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            using (var db = _context.CreateUnitOfWork())
            {
                _materialRepository.Add(request.Isbn, request.Title, request.Description, request.Edition);

                db.SaveChanges();
            }

            return Task.Run(() => Unit.Value, cancellationToken);
        }

    }
}
