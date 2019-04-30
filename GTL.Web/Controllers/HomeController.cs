using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GTL.Web.Models;
using GTL.Application.Users.Queries.GetUser;
using GTL.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using GTL.Application.Exceptions;
using GTL.Application.UseCases.Users;
using GTL.Application.UseCases.Users.Commands.CreateUser;
using GTL.Application.UseCases.Users.Commands.DeleteUser;
using GTL.Application.UseCases.Users.Commands.UpdateUser;
using GTL.Application.UseCases.Users.Queries.GetUser;
using GTL.Application.UseCases.Users.Queries.GetUserList;
using GTL.Application.ViewModels;

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

        [Authorize(Policy = "CanReadUsers")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await Mediator.Send(new GetUserDetailQuery { Id = id ?? default(int) });
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "CanWriteUsers")]
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
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (id != model.User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var command = new UpdateUserCommand
                {
                    Id = model.User.Id,
                    Name = model.User.Name,
                    City = model.User.City,
                    ZipCode = model.User.ZipCode,

                };
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (AuthException)
            {
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
