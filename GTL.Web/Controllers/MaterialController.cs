using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.UseCases.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTL.Web.Controllers
{
    public class MaterialController : Controller
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaterialBaseCommand command)
        {
            try
            {
                command.Title = "Test";
                command.Description = "Test description";
                command.Edition = 1;
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