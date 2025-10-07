using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.BLL.Repositories;
using Mvc.DAL.Models;

namespace Mvc.PAL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = departmentRepo.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepo.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentRepo.GetById(id.Value);
            if (department is null)
                return NotFound();
             return View(department);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentRepo.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }


        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentRepo.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    departmentRepo.Delete(department);
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
