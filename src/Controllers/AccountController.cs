using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerING.Interfaces;
using ServerING.Mocks;
using ServerING.Models;
using ServerING.Services;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ServerING.Controllers {
    public class AccountController : Controller {

        IUserService userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger) {
            this.userService = userService;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                try {
                    User user = new User {
                        Login = model.Login,
                        Password = model.Password,
                        Role = "User"
                    };

                    userService.AddUser(user);

                    await Authenticate(user);

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

                    return RedirectToAction("Index", "Home");

                } catch (Exception ex) {
                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1} - Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        ex.Message);

                    ModelState.AddModelError("", "Логин занят");
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {

                User user = userService.ValidateUser(model);

                if (user != null) {
                    await Authenticate(user); // аутентификация

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

                    return RedirectToAction("Index", "Home");
                }

                _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1} - Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        "Incorrect Login or Password");

                ModelState.AddModelError("", "Incorrect Login or Password");

                return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout() {
            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user) {
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
            };

            // Crete ClaimsIdentity object
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // Enable cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
