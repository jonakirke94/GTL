using AutoMapper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Mapping;
using GTL.Domain.Entities;

namespace GTL.Application.UseCases.Users.Queries
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}

