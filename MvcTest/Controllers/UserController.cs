using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Mvc.DAL.Models;
using Mvc.PAL.ViewModels;

namespace Mvc.PAL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Users = await userManager.Users.Select(
                    U => new userViewModel()
                    {
                        Id = U.Id,
                        FName = U.FName,
                        LName = U.LName,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Roles = userManager.GetRolesAsync(U).Result
                    }).ToListAsync();
                return View(Users);
            }
            else
            {
                var User = await userManager.FindByEmailAsync(SearchValue);
                //Manual Mapping
                var mappedUser = new userViewModel()
                {
                    Id = User.Id,
                    FName = User.FName,
                    LName = User.LName,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = userManager.GetRolesAsync(User).Result
                };
                return View(new List<userViewModel> { mappedUser});
            }
                return View();
        }
    }
}
