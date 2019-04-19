using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.UseCases.Users;

namespace GTL.Application.Users.Queries.GetUser
{
    public class GetUserDetailQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
}

