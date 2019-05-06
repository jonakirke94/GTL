using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using GTL.Application.UseCases.Loans.Commands.CreateLoan;

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
            command.Loan.LoanDate = DateTime.Now;

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


  

            TempData["Status"] = "Loan was successfully created";
            return View();
        }
    }
}