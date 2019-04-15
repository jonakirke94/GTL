using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Queries.GetUser
{
    public class GetUserDetailQuery : IRequest<UserDetailModel>
    {
        public int Id { get; set; }
    }
}

