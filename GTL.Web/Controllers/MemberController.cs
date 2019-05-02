using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GTL.Application.UseCases.Members.Commands.CreateMember;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GTL.Web.Controllers
{
    public class MemberController : BaseController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberCommand command)
        {
            command.Ssn = null;

            try
            {
                await Mediator.Send(command);
            }
            catch (GTL.Application.Exceptions.ValidationException e)
            {                
                ModelState.AddModelError("", e.Failures.FirstOrDefault().Value[0]);
                return View();
            }
            catch (Exception e)
            {
                TempData["Status"] = "Something unexpected happened";
                return View();
            }

            TempData["Status"] = "Succesfully created member";
            return View();
        }
    }
}
