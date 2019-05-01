using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GTL.Application.UseCases.Users;
using GTL.Application.UseCases.Users.Queries.GetUser;
using GTL.Application.ViewModels;

namespace GTL.Application.Users.Queries.GetUser
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepo.GetUserByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return _mapper.Map<UserViewModel>(entity);
        }
    }
}
