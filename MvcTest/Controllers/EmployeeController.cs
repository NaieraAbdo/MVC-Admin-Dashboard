using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.DAL.Models;

namespace Mvc.PAL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IDepartmentRepo departmentRepo;

        public EmployeeController(IEmployeeRepo employeeRepo,IDepartmentRepo departmentRepo)
        {
            this.employeeRepo = employeeRepo;
            this.departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var emps = employeeRepo.GetAll();
            return View(emps);
        }

        public IActionResult Create()
        {
            //ViewBag.departments = departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //if (ModelState.IsValid)
            //Department is Invalid
            //{
                employeeRepo.Add(emp);
                return RedirectToAction(nameof(Index));
            //}
            //return View(emp);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = employeeRepo.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return View(ViewName, employee);
        }

        public  IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeRepo.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            //ViewBag.depts = departmentRepo.GetAll();
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return  Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int? id)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                employeeRepo.Delete(employee);


                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }

        }
    }
}
