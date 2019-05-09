using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Features.Login.Commands;
using GTL.Application.Interfaces.Authentication;
using GTL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GTL.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ISignInManager _signInManager;

        public AccountController(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            try
            {
                var isSuccess = await Mediator.Send(command);

                if (isSuccess)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("Password", "Invalid email or password");
            }
            catch (ValidationException e)
            {
                ModelState.AddValidationErrors(e);
            }


            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
