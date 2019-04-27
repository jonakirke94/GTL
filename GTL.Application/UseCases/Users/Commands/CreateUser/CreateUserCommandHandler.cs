﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;

namespace GTL.Application.UseCases.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;

        public CreateUserCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
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
                LastChanged = DateTime.Now,
                PermissionLevel = request.PermissionLevel
            };

            await _userRepo.CreateAsync(entity, cancellationToken);

            //await _mediator.Publish(new UserCreated { UserId = userId }, cancellationToken);

            return Unit.Value;
        }
    }
}