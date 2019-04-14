using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
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
        private readonly IUserManager _userManager;
        private readonly IMediator _mediator;

        public Handler(IUserManager userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var salt = Hasher.CreateSalt();
            var passwordHash = Hasher.Hash(request.Password, salt);

            var entity = new User
            {
                Name = request.Name,
                NormalizedName = request.Name.Normalize(),
                Email = request.Email,
                NormalizedEmail = request.Email.Normalize(),
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                LastChanged = DateTime.Now
            };

             await _userManager.CreateAsync(entity, cancellationToken);

            //await _mediator.Publish(new UserCreated { UserId = userId }, cancellationToken);

            return Unit.Value;
        }
    }
}
