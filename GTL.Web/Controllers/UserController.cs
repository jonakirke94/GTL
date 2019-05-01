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
                // redirect to access denied
                //return ();
            }
            catch (ValidationException)
            {
                // return bad request
            }catch(Exception e)
            {
                //return something unexpected happended
            }



            return View();
        }
    }
}