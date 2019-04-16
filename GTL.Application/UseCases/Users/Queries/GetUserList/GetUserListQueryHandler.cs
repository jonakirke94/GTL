using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GTL.Application.Interfaces.Repositories;
using MediatR;

namespace GTL.Application.UseCases.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListDto>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserListDto> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return new UserListDto
            {
                Users = _mapper.Map<IEnumerable<UserDto>>(await _userRepo.GetUsersAsync(cancellationToken))
            };
        }
    }
}


