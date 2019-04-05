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
        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Name { get; set; }        
    }
}

