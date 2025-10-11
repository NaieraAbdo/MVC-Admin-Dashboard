using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.BLL.Repositories;
using Mvc.DAL.Models;

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
        public IActionResult Index()
        {
            var departments = unitOfWork.DepartmentRepo.GetAll();
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
                unitOfWork.DepartmentRepo.Add(department);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = unitOfWork.DepartmentRepo.GetById(id.Value);
            if (department is null)
                return NotFound();
             return View(department);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = unitOfWork.DepartmentRepo.GetById(id.Value);
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
                    unitOfWork.DepartmentRepo.Update(department);
                    unitOfWork.Complete();
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
            var department = unitOfWork.DepartmentRepo.GetById(id.Value);
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
                    unitOfWork.DepartmentRepo.Delete(department);
                    unitOfWork.Complete();
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
