using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.Members.Commands.CreateMember;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GTL.Web.Controllers
{
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
            }
            catch (Application.Exceptions.ValidationException e)
            {
                ModelState.AddModelError("", e.Failures.FirstOrDefault().Value[0]);
                return View();
            }
            catch (NotUniqueSsnException e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Something unexpected happened. Please try again");
                return View();
            }

            TempData["Status"] = "Successfully created member";
            return View();
        }
    }
}
