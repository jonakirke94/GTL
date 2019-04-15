using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Domain.Entities.Identity;

namespace GTL.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand()
        {
            Roles = new List<Role>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }   
        
        public IList<Role> Roles { get; private set; }
    }
}

