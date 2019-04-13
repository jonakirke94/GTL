using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepo;
        private readonly IBookRepository _bookRepo;

        public DeleteUserCommandHandler(IUserRepository userRepo, IBookRepository bookRepo)
        {
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var entity = _userRepo.GetUser(request.Id);

            //if (entity == null)
            //{
            //    throw new NotFoundException(nameof(User), request.Id);
            //}

            //var userBooks = await _bookRepo.GetBooksByUserId(request.Id);
            //if (userBooks.Count > 0)
            //{
            //    // TODO: Add functional test for this behaviour.
            //    throw new DeleteFailureException(nameof(User), request.Id, "There are existing orders associated with this customer.");
            //}

            _userRepo.DeleteUser(request.Id);

            return Unit.Value;
        }
    }
}
