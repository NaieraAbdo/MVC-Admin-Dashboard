using Mvc.BLL.Interfaces;
using Mvc.DAL.Contexts;
using Mvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.BLL.Repositories
{
    public class EmployeeRepo: GenericRepo<Employee> , IEmployeeRepo
    {
        private readonly MvcDbcontext dbcontext;

        public EmployeeRepo(MvcDbcontext dbcontext):base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IQueryable<Employee> GetEmployeesByName(string employeeName)
     =>  dbcontext.Employees.Where(e => e.Name.ToLower().Contains(employeeName.ToLower()));
        
    }
}
