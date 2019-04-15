using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.UseCases.Account.Commands.Login;
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

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var signInResult = await Mediator.Send(new LoginCommand { Email = model.Email, Password = model.Password, IsPersistent = model.IsPersistent });

                if (signInResult.Success)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    TempData["LoginResult"] = "Invalid email or password";
                    // may wanna log incorrect login attempt
                }
            }

            //log user logged in
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
