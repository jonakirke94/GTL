using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Commands.CreateUser
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
            if (!request.Roles.Any())
            {
                throw new NoRoleException(request.Name);
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
                LastChanged = DateTime.Now
            };

            var userId = await _userRepo.CreateAsync(entity, cancellationToken);

            var allRoles = await _roleRepo.GetAllRolesAsync(cancellationToken);
            var userRolesList = new List<UserRole>();

            foreach (var userRoles in request.Roles)
            {
                var role = allRoles.SingleOrDefault(x => x.Name == userRoles.Name);

                if (role == null)
                {
                    throw new NoRoleMatchException(userRoles.Name);
                }

                userRolesList.Add(new UserRole
                {
                    UserId = userId,
                    RoleId = role.Id
                });
            }

            await _roleRepo.AddRolesToUser(userRolesList, cancellationToken);


            //await _mediator.Publish(new UserCreated { UserId = userId }, cancellationToken);

            return Unit.Value;
        }
    }
}
