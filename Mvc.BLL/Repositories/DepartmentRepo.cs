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
    public class DepartmentRepo :GenericRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(MvcDbcontext dbcontext):base(dbcontext)
        {
            
        }
    }
}
