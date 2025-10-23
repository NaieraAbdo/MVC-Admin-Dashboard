using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.PAL.ViewModels;
using System.Data;
using System.Threading.Tasks;

namespace Mvc.PAL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(String SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await roleManager.Roles.ToListAsync();
                var mappedRole = mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RoleViewModel>>(Roles);
                return View(mappedRole);
            }
            else
            {
                var Role = await roleManager.FindByNameAsync(SearchValue);
                var mappedRole = mapper.Map < IdentityRole, RoleViewModel > (Role);
                return View(new List<RoleViewModel>(){ mappedRole} );

            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = mapper.Map<RoleViewModel, IdentityRole>(model);
                await roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
                return View(model);
        }

        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var role = await roleManager.FindByIdAsync(Id);
            if (role is null)
                return NotFound();
            var mappedRole = mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(ViewName, mappedRole);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(id);
                    role.Name = model.RoleName;
                    role.Id = model.Id;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
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
                var role = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }

    }
}
