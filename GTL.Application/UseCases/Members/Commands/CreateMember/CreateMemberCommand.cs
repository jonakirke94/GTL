using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.UseCases.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest
    {
        public CreateMemberCommand()
        {
            Address = new Address();
        }

        public string Ssn { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
