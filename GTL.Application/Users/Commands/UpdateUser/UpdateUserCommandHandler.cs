﻿using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;

        public UpdateUserCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _userRepo.GetUser(request.Id);

            //if (entity == null)
            //{
            //    throw new NotFoundException(nameof(User), request.Id);
            //}

            entity.Name = request.Name;
            entity.City = request.City;
            entity.ZipCode = request.ZipCode;

            _userRepo.Update(entity);


            return Unit.Value;
        }
    }
}