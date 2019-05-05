using System;
using System.Collections.Generic;
using AutoMapper;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Mapping;
using GTL.Application.UseCases.Users.Queries;
using GTL.Domain.Entities;
using GTL.Domain.Enums;

namespace GTL.Application.ViewModels
{
    public class UserListViewModel : IHaveCustomMapping
    {
        public IEnumerable<UserDto> Users { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IEnumerable<User>, UserListViewModel>()
                .ForMember(pDto => pDto.EditEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(pDto => pDto.DeleteEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(pDTO => pDTO.Users, opt => opt.MapFrom(x => x));
        }

        class PermissionsResolver : IValueResolver<IEnumerable<User>, UserListViewModel, bool>
        {
            private readonly ICurrentUser _currentUser;

            public PermissionsResolver(ICurrentUser currentUser)
            {
                _currentUser = currentUser;
            }

            public bool Resolve(IEnumerable<User> source, UserListViewModel dest, bool destMember, ResolutionContext context)
            {
                try
                {
                    return _currentUser.GetCurrentRole() >= Role.ASSOCIATELIBRARIAN;
                }
                catch (NotInRoleException)
                {
                    return false;
                }
            }
        }
    }
}
