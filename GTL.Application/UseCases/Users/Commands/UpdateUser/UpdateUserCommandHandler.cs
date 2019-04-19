using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IUserRepository userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepo.GetUserByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            entity.Name = request.Name;
            entity.City = request.City;
            entity.ZipCode = request.ZipCode;

            await _userRepo.UpdateUserAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
