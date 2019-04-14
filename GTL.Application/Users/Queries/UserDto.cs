﻿using AutoMapper;
using GTL.Application.Interfaces.Mapping;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Users.Queries.GetUserList
{
    public class UserDto : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(pDTO => pDTO.EditEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(pDTO => pDTO.DeleteEnabled, opt => opt.MapFrom<PermissionsResolver>());
        }

        class PermissionsResolver : IValueResolver<User, UserDto, bool>
        {
            // TODO: inject your services and helper here
            public PermissionsResolver()
            {

            }

            public bool Resolve(User source, UserDto dest, bool destMember, ResolutionContext context)
            {
                return false;
            }
        }
    }
}
