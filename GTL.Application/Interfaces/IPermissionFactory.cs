using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using MediatR;

namespace GTL.Application.Interfaces
{
    public interface IPermissionFactory
    {
        PermissionLevel GetPermissionByRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
    }
}
