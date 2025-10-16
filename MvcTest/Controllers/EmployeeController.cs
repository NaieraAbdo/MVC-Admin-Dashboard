using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mvc.BLL.Interfaces;
using Mvc.DAL.Models;
using Mvc.PAL.Helpers;
using Mvc.PAL.ViewModels;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> emps;
            //if(searchName is not null)
            if (string.IsNullOrEmpty(SearchValue))
                emps = await unitOfWork.EmployeeRepo.GetAllAsync();
                    
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
        public async Task<IActionResult> Create(EmployeeViewModel empVM)
        {
            //if (ModelState.IsValid)
            //Department is Invalid
            //{
            empVM.ImageName = DocumentSettings.UploadFile(empVM.Image, "Images");
            var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(empVM);
            await unitOfWork.EmployeeRepo.AddAsync(mappedEmp);
            await unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(emp);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = await unitOfWork.EmployeeRepo.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mappedEmp);
        }

        public  async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            //if (ModelState.IsValid)
            //{
                try
                {
                    if(employeeVM.Image is not null)
                {
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                }
                    var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    unitOfWork.EmployeeRepo.Update(mappedEmp);
                    await unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            //}
            //ViewBag.depts = departmentRepo.GetAll();
            return View(employeeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int? id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                unitOfWork.EmployeeRepo.Delete(mappedEmp);
               var Result = await unitOfWork.CompleteAsync();
                if(Result>0 && employeeVM.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                }


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
