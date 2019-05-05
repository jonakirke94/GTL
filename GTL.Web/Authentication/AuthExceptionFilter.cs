using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Role = GTL.Domain.Enums.Role;

namespace GTL.Web
{
    public class AuthExceptionFilter : IExceptionFilter
    {
    
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType().Name;

            if (exceptionType == "AuthenticationException")
            {
                context.ExceptionHandled = true;
                context.Result = new RedirectToActionResult("Login", "Account", null);

            }

            if (exceptionType == "AuthorizeException")
            {
                // may want to send route values with exceptions
                context.ExceptionHandled = true;
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }



        }
    }
}
