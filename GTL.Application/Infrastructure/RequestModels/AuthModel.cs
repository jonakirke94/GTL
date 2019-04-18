using System;
using System.Collections.Generic;
using System.Text;
using GTL.Domain.Entities;

namespace GTL.Application.Infrastructure.RequestModels
{
    public class AuthModel
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAuthorized { get; set; }
        public PermissionLevel UserPermissionLevel { get; set; }
        public PermissionLevel RequiredMinPermissionLevel { get; set; }
        public string ErrorMessage { get; set; }
    }
}


