using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.LoanerCard.Commands.CreateLoanerCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GTL.Web.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Policy = "CanCreateLoanerCard")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateLoanerCard(string ssn)
        {
            try
            {
                var command = new CreateLoanerCardCommand {Ssn = ssn};
                await Mediator.Send(command);
            }
            catch (AuthException)
            {
                return AccessDenied();
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError("Validation Exception", e.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Unexpected Error", e.Message);
                // redirect to edit view
                return View();
            }

            // redirect to edit view
            TempData["Message"] = "Loaner card was successfully created";
            return View();
        }
    }
}