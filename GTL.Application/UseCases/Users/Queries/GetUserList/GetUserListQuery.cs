using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListViewModel>
    {
    }
}
