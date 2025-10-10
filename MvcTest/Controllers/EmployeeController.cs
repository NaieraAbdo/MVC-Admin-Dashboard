using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.DAL.Models;
using Mvc.PAL.ViewModels;

namespace Mvc.PAL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IEmployeeRepo employeeRepo;
        //private readonly IDepartmentRepo departmentRepo;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork
            //IEmployeeRepo employeeRepo,IDepartmentRepo departmentRepo
            ,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            //this.employeeRepo = employeeRepo;
            //this.departmentRepo = departmentRepo;
            this.mapper = mapper;
        }
        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> emps;
            //if(searchName is not null)
            if (string.IsNullOrEmpty(SearchValue))
                emps = unitOfWork.EmployeeRepo.GetAll();
                    
            else
                emps = unitOfWork.EmployeeRepo.GetEmployeesByName(SearchValue);

            var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emps);
            return View(mappedEmps);
        }

        public IActionResult Create()
        {
            //ViewBag.departments = departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel empVM)
        {
            //if (ModelState.IsValid)
            //Department is Invalid
            //{
            var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(empVM);
            unitOfWork.EmployeeRepo.Add(mappedEmp);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
            //}
            //return View(emp);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = unitOfWork.EmployeeRepo.GetById(id.Value);
            if (employee is null)
                return NotFound();
            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mappedEmp);
        }

        public  IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    unitOfWork.EmployeeRepo.Update(mappedEmp);
                    unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            //ViewBag.depts = departmentRepo.GetAll();
            return View(employeeVM);
        }

        public IActionResult Delete(int? id)
        {
            return  Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int? id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.EmployeeRepo.Delete(mappedEmp);
                unitOfWork.Complete();


                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }

        }
    }
}
