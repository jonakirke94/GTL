using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.CreateUser
{
    public class Handler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMediator _mediator;

        public Handler(IUserRepository userRepo, IRoleRepository roleRepo, IMediator mediator)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RoleName))
            {
                throw new NoRoleException(request.Name);
            }

            var role = await _roleRepo.GetRoleByNameAsync(request.RoleName);

            if (role == null)
            {
                throw new NoRoleMatchException(request.RoleName);
            }

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
                LastChanged = DateTime.Now,
                RoleId = role.Id,
            };

            await _userRepo.CreateAsync(entity, cancellationToken);

            //await _mediator.Publish(new UserCreated { UserId = userId }, cancellationToken);

            return Unit.Value;
        }
    }
}
