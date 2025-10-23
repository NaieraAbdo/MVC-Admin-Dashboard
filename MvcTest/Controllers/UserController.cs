using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Mvc.DAL.Models;
using Mvc.PAL.ViewModels;
using System.Threading.Tasks;

namespace Mvc.PAL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
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
        }

        public async Task<IActionResult> Details(string Id ,string ViewName ="Details")
        {
            if (Id is null)
                return BadRequest();
         var User = await userManager.FindByIdAsync(Id);
            if (User is null)
                return NotFound();
            var mappedUser = mapper.Map<ApplicationUser,userViewModel>(User);
            return View(ViewName,mappedUser);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id,"Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit (userViewModel model, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var mappedUser = mapper.Map<userViewModel, ApplicationUser>(model); wroooooong
                    var User = await userManager.FindByIdAsync(id);
                    User.PhoneNumber = model.PhoneNumber;
                    User.FName = model.FName;
                    User.LName = model.LName;
                    await userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var User = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error","Home");
            }
        }
    }
}
