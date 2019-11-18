using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        [Authorize(Roles = "BranchManager, Admin")]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "BranchManager, Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = model.Email, 
                    Email = model.Email,
                    City = model.City
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListRoles", "Administration");
                    }
                    result = await userManager.AddToRoleAsync(user, "Manager");
                    if (result.Succeeded)
                    {
                    return RedirectToAction("ListUsers", "Administration");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Error Occured while adding {user.UserName} to role <b>Manager</b>";
                        return View("NotFoundError");
                    }

                    //To Login User after registering
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Employee");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
           return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json($"Email {email} is already in use.");
        }
        

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && (Url.IsLocalUrl(returnUrl)))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Employee");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }   
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
