using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Mapping;
using GTL.Application.UseCases.Users.Queries;
using GTL.Domain.Entities;

namespace GTL.Application.UseCases.Users
{
    public class UserViewModel : IHaveCustomMapping
    {
        public UserDto User { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(pDTO => pDTO.EditEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(pDTO => pDTO.DeleteEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(pDTO => pDTO.User, opt => opt.MapFrom(x => x));
        }

        class PermissionsResolver : IValueResolver<User, UserViewModel, bool>
        {
            private readonly ICurrentUser _currentUser;

            public PermissionsResolver(ICurrentUser currentUser)
            {
                _currentUser = currentUser;
            }

            public bool Resolve(User source, UserViewModel dest, bool destMember, ResolutionContext context)
            {
                return _currentUser.GetCurrentPermission() >= PermissionLevel.CHIEFLIBRARIAN;
            }
        }
    }
}
