using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.Commands;
using GTL.Domain.Exceptions;
using GTL.Web.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GTL.Web.Controllers
{
    public class MaterialController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // TODO Uncomment line below to enable authentication
        //[Authorize(Roles = RoleHierarchy.ASSOCIATELIBRARIAN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialBaseCommand command)
        {
            try
            {
                await Mediator.Send(command);
                TempData["Status"] = "Successfully created material";
            }
            catch (ISBNAlreadyExistException)
            {
                ModelState.AddModelError("ISBN", "This ISBN is already created in the database");
            }
            catch (ValidationException e)
            {
                ModelState.AddValidationErrors(e);
            }

            return View();
        }
    }
}