using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task<IActionResult> Register(string userName, string userEmail,
            string userPassword)
        {
            try
            {
                ViewBag.Message = "User already registered";
                AppUser user = await UserManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    user = new AppUser();
                    user.Email = userEmail;
                    user.UserName = userName;

                    IdentityResult result = await UserManager.CreateAsync(user, userPassword);
                    ViewBag.Message = "User was created";
                    return View("LoginForm");
                }
            }
            catch(Exception exeption)
            {
                ViewBag.Message = exeption.Message;
            }
            return View("RegisterForm");
        }

        public IActionResult RegisterForm()
        {
            return View();
        }

        public async Task<IActionResult> Login(string loginEmail, string loginPassword)
        {
            AppUser user = await UserManager.FindByEmailAsync(loginEmail);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, loginPassword, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            
            ViewBag.Message = "Incorrect email or password";

            return View("LoginForm");
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}
