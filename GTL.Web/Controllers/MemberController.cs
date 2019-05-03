using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.Members.Commands.CreateMember;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ValidationException = GTL.Application.Exceptions.ValidationException;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GTL.Web.Controllers
{
    [ServiceFilter(typeof(AuthExceptionFilter))]
    public class MemberController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberCommand command)
        {
            try
            {
                await Mediator.Send(command);
                TempData["Status"] = "Successfully created member";
            }
            catch (NotUniqueSsnException)
            {
                ModelState.AddModelError("Ssn", "A member with this ssn already exists");
            }
            catch (ValidationException e)
            {
                ModelState.AddValidationErrors(e);
            }

            return View();
        }
    }
}
