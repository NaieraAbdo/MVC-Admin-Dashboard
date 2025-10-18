using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.DAL.Migrations;
using Mvc.DAL.Models;
using Mvc.PAL.Helpers;
using Mvc.PAL.ViewModels;
using System.Threading.Tasks;
using ApplicationUser = Mvc.DAL.Models.ApplicationUser;

namespace Mvc.PAL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };
                var Result = await userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
              
            }
            return View(model);
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var IsMatch = await userManager.CheckPasswordAsync(User, model.Password);
                    if (IsMatch)
                    {
                        var Result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (Result.Succeeded)                      
                            return RedirectToAction("Index", "Home");                       
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Incorrect Password.");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email doesn't Exist.");                
            }
            return View(model);
        }


        //  SIGNOUT
        public new async Task<IActionResult> SignOut()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        //Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(User);
                    var ResetPasswordLink = Url.Action("ResetPassword","Account",new {email = User.Email, token = Token},Request.Scheme);
                    //Send Email
                    var Email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = "Reset Password Link"
                    };
                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                    ModelState.AddModelError(string.Empty, "Email doesn't exist.");
             
            }
            return View("ForgetPassword",model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user = await userManager.FindByEmailAsync(email);
                var Result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}
