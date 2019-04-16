using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GTL.Application.Authentication;
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

        public int GetUserId()
        {
            var claim = _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return -1;

            int.TryParse(claim.Value, out var currentId);

            return currentId;
        }
    }
}
