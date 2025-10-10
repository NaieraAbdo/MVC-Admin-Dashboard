using Mvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.BLL.Interfaces
{
    public interface IEmployeeRepo: IGenericRepo<Employee>
    {
        IQueryable<Employee> GetEmployeesByName(string employeeName);
    }
}
