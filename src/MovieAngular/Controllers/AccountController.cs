﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MovieAngular.Models;

namespace MovieAngular.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            var signInStatus = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (signInStatus == SignInResult.Success)
            {
                return Redirect("/home");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }


        public IActionResult SignOut()
        {
            _signInManager.SignOut();
            return Redirect("/home");
        }
    }
}