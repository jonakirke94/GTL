using AutoMapper;
using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListViewModel>
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserListViewModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return new UserListViewModel
            {
                Users = _mapper.Map<IEnumerable<UserDto>>(await _userManager.GetUsersAsync(cancellationToken))
            };
        }
    }
}


