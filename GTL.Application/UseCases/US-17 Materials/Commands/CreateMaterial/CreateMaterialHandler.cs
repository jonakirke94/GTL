using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.ValueObjects;
using MediatR;

namespace GTL.Application.UseCases.Commands.CreateMaterial
{
    public class CreateMaterialHandler : IRequestHandler<MaterialBaseCommand, Unit>
    {
        private readonly IGTLContext _context;
        private readonly IMaterialRepository _materialRepository;

        public CreateMaterialHandler(IGTLContext context, IMaterialRepository materialRepository)
        {
            _context = context;
            _materialRepository = materialRepository;
        }

        public Task<Unit> Handle(MaterialBaseCommand request, CancellationToken cancellationToken)
        {
            using (var db = _context.CreateUnitOfWork())
            {
                ISBN ISBN = null;
                if (!string.IsNullOrWhiteSpace(request.Isbn))
                {
                    ISBN = ISBN.For(request.Isbn);
                }

                Material material = new Material
                {
                    ISBN = ISBN,
                    Title = request.Title,
                    Description = request.Description,
                    Edition = request.Edition
                };

                _materialRepository.Add(material);

                db.SaveChanges();
            }

            return Task.Run(() => Unit.Value, cancellationToken);
        }

    }
}
