using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GTL.Web.Models;
using GTL.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using GTL.Application.Exceptions;
using System.Collections.Generic;
using GTL.Web.Helpers;

namespace GTL.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISignInManager _signInManager;

        public HomeController(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            // var users = await Mediator.Send(new GetUserListQuery());
            // return View(users);

            ViewBag.Status = new Status { Type = Type.danger, Message = "Check home controller index() for how to generate status messages" };


            return View();
        }

            
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
