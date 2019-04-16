using System.Collections.Generic;
using GTL.Application.Authorization;

namespace GTL.Application.UseCases.Users.Queries.GetUserList
{
    public class UserListDto : CommandResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}

