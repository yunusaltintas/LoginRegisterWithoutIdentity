
using LoginRegister.Services;
using LoginRegister.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace LoginRegister.Controllers
{


    public class HomeController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly ISmtpService _smtpService;

        public HomeController(IRegisterService registerService, ISmtpService smtpService)
        {
            _registerService = registerService;
            _smtpService = smtpService;
        }



        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Privacy()
        {

            return View();
        }


        [HttpGet]
        public IActionResult RegisterAsync()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(ViewRegisterModel ViewRegisterModels)
        {

            if (ViewRegisterModels.Password1 != ViewRegisterModels.Password2)
            {

                return RedirectToAction("Register");
            }

            var result = await _registerService.CreateUserAsync(ViewRegisterModels);

            if (result != true)
            {
                return RedirectToAction("Register");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(ViewLoginModel ViewLoginModel)
        {
           
            var result = await _registerService.Login(ViewLoginModel);

            if (result == null)
            {
                return RedirectToAction("login");
            }

            List<Claim> userClaims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
            new Claim(ClaimTypes.Name, result.UserName),
            new Claim(ClaimTypes.Email, result.Email.ToString())
            };
            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
            {
                IsPersistent = ViewLoginModel.RememberMe
            });


            return RedirectToAction("Privacy");
        }


        [HttpGet]
        public IActionResult Forget()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Forget(ViewForgetModel viewForgetModel)
        {
            await _smtpService.ForgetPasswordAsync(viewForgetModel.Email);
            return RedirectToAction("login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ViewChangePasswordModel viewChangePasswordModel)
        {
            if (await _registerService.ChangePassword(viewChangePasswordModel))
            {
                return RedirectToAction("Login");
            }


            return View();
        }
    }
}
