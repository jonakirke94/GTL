using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTL.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }        
    }
}

