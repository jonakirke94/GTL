using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;
        private readonly IBookRepository _bookRepo;

        public DeleteUserCommandHandler(IUserRepository userRepo, IAuthService authService, IBookRepository bookRepo)
        {
            _userRepo = userRepo;
            _authService = authService;
            _bookRepo = bookRepo;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userRepo.GetUserByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var userBooks = await _bookRepo.GetBooksByUserId(request.Id);
            if (userBooks.Count > 0)
            {
                // TODO: Add functional test for this behaviour.
                throw new DeleteFailureException(nameof(User), request.Id, "There are existing loans associated with this member.");
            }

            await _userRepo.DeleteUserAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
