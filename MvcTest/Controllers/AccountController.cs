using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.DAL.Migrations;
using Mvc.DAL.Models;
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

    }
}
