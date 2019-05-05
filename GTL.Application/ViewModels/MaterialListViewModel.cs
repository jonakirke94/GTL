using System.Collections.Generic;
using AutoMapper;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.UseCases.Commands;
using GTL.Domain.Entities;
using GTL.Domain.Enums;

namespace GTL.Application.ViewModels
{
    public class MaterialListViewModel
    {
        public IEnumerable<MaterialBaseCommand> Materials { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IEnumerable<Material>, MaterialListViewModel>()
                .ForMember(x => x.EditEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(x => x.DeleteEnabled, opt => opt.MapFrom<PermissionsResolver>())
                .ForMember(x => x.Materials, opt => opt.MapFrom(x => x));
        }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public int Edition { get; set; }
    }

    public class PermissionsResolver : IValueResolver<IEnumerable<Material>, MaterialListViewModel, bool>
    {
        private readonly ICurrentUser _currentUser;

        public PermissionsResolver(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public bool Resolve(IEnumerable<Material> source, MaterialListViewModel dest, bool destMember,
            ResolutionContext context)
        {
            try
            {
                return _currentUser.GetCurrentRole() >= Role.CHECKOUTSTAFF;
            }
            catch (NotInRoleException)
            {
                return false;
            }
        }

    }
}
