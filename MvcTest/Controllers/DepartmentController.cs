using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.BLL.Repositories;
using Mvc.DAL.Models;
using System.Threading.Tasks;

namespace Mvc.PAL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IDepartmentRepo departmentRepo;

        public DepartmentController( IUnitOfWork unitOfWork
            //IDepartmentRepo departmentRepo
            )
        {
            this.unitOfWork = unitOfWork;
            //this.departmentRepo = departmentRepo;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepo.GetAllAsync();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
              await unitOfWork.DepartmentRepo.AddAsync(department);
               await unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await unitOfWork.DepartmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
             return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await unitOfWork.DepartmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.DepartmentRepo.Update(department);
                   await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await unitOfWork.DepartmentRepo.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.DepartmentRepo.Delete(department);
                   await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
