using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application
{
    public class PermissionFactory : IPermissionFactory
    {
        public PermissionLevel GetPermissionByRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            var interfaces = request.GetType().GetInterfaces();

            var requestInterface = interfaces[0];

            switch (requestInterface.Name)
            {
                case "IAdminRequest":
                    return PermissionLevel.CHIEFLIBRARIAN;
                case "IMemberRequest":
                    return PermissionLevel.REFERENCELIBRARIAN;
                default:
                    return PermissionLevel.DEFAULT;
            }
        }
    }
}
