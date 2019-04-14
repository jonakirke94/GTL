using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GTL.Web.Models;
using GTL.Application.Users.Queries.GetUserList;
using GTL.Application.Users.Commands.DeleteUser;
using GTL.Application.Users.Queries.GetUser;
using GTL.Application.Users.Commands.CreateUser;
using GTL.Application.Users.Commands.UpdateUser;
using GTL.Domain.Entities;
using GTL.Application.Interfaces.Authentication;

namespace GTL.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISignInManager _signInManager;

        public HomeController(ISignInManager signInManager)
        {
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Index()
        {

         
            var users = await Mediator.Send(new GetUserListQuery());
            return View(users);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await Mediator.Send(new GetUserDetailQuery { Id = id ?? default(int) });
            return View(user);
        }


        public IActionResult Login()
        {
            return View();        
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await _signInManager.SignInAsync(model.Email, model.Password, model.isPersistent);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await Mediator.Send(new GetUserDetailQuery { Id = id ?? default(int) });
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDetailModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var command = new UpdateUserCommand
                {
                    Id = user.Id,
                    Name = user.Name,
                    City = user.City,
                    ZipCode = user.ZipCode,

                };
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var user = await Mediator.Send(new GetUserDetailQuery { Id = id ?? default(int)});
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
