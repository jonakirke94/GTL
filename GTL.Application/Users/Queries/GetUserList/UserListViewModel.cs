using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTL.Application.Users.Queries.GetUserList
{
    public class UserListViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}

