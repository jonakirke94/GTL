using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Features.Loans.Commands.CreateLoan;
using GTL.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Type = GTL.Web.Models.Type;

namespace GTL.Web.Controllers
{
    public class LoanController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateLoan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLoan(CreateLoanCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);
                if (response.HasRequestError)
                {
                    ModelState.AddModelError("", response.ErrorMessage);
                    return View();
                }
            }
            catch (ValidationException e)
            {
                ModelState.AddValidationErrors(e);
                return View();
            }

            ViewBag.Status = new Status { Type = Type.success, Message = "Loan was successfully created" };
            return View();
        }
    }
}