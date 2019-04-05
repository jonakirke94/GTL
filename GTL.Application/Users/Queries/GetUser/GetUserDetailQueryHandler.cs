using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Queries.GetUser
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailModel>
    {
        private readonly IUserRepository _userRepo;

        public GetUserDetailQueryHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDetailModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = _userRepo.GetUser(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return UserDetailModel.Create(entity);
        }
    }
}
