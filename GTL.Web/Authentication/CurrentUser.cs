﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Interfaces.Authentication;
using GTL.Domain.Entities;
using GTL.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace GTL.Web.Authentication
{
    public class CurrentUser : ICurrentUser
    {

        private readonly IHttpContextAccessor _context;

        public CurrentUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        public bool IsAuthenticated()
        {
            return _context.HttpContext.User.Identity.IsAuthenticated;
        }

        public Role GetCurrentRole()
        {
            var claim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (claim == null)
                throw new NotInRoleException();

            Enum.TryParse(claim.Value, out Role inRole);
            return inRole;
        }

        public int GetId()
        {
            var claim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim == null ? -1 : int.Parse(claim.Value);
        }
    }
}
