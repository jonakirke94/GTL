using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Application.Exceptions;

namespace GTL.Web
{
    public static class ValidationHelper
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, ValidationException e)
        {
            modelState.AddModelError(string.Empty, e.Message);

            foreach (var (key, value) in e.Failures)
            {
                foreach (var error in value)
                {
                    modelState.AddModelError(key, error);
                }
            }

        }
    }
}
