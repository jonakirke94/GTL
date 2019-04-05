using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Commands.CreateUser
{
    public class Handler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMediator _mediator;

        public Handler(IUserRepository userRepo, IMediator mediator)
        {
            _userRepo = userRepo;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                City = request.City,
                ZipCode = request.ZipCode,
                Name = request.Name
            };

            var userId =_userRepo.AddUser(entity);

            await _mediator.Publish(new UserCreated { UserId = userId }, cancellationToken);

            return Unit.Value;
        }
    }
}
