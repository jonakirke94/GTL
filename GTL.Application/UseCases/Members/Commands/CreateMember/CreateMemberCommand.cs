using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Helper.CustomAttributes;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.UseCases.Members.Commands.CreateMember
{
    [Authorize(Role.ASSOCIATELIBRARIAN)]
    public class CreateMemberCommand : IRequest
    {
        public string Ssn { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string StreetName { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public AddressType AddressType { get; set; }
    }
}
