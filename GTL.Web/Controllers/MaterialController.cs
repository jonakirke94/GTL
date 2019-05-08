using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.Commands;
using GTL.Application.ViewModels;
using GTL.Domain.Exceptions;
using GTL.Web.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GTL.Web.Controllers
{
    public class MaterialController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            //var materials = await Mediator.Send(new GetMaterialListQuery());
            //return View(materials);

            var vm = new MaterialListViewModel
            {
                DeleteEnabled = true,
                EditEnabled = true,
                Materials = new List<MaterialBaseCommand>()
            };

            return View(vm);
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            throw new NotImplementedException();

            //var user = await Mediator.Send(new GetUserDetailQuery {Id = id ?? default(int)});
            //return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            throw new NotImplementedException();
            //var user = await Mediator.Send(new GetUserDetailQuery { Id = id ?? default(int) });
            //return View(user);
        }

    }
}