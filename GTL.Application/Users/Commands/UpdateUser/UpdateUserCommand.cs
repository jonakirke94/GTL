using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
