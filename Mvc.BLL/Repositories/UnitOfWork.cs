using Mvc.BLL.Interfaces;
using Mvc.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MvcDbcontext dbcontext;

        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }

        public UnitOfWork(MvcDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
            EmployeeRepo = new EmployeeRepo(dbcontext);
            DepartmentRepo = new DepartmentRepo(dbcontext);
        }
        public void Dispose()
        {
            dbcontext.Dispose();
        }

        public async Task<int> CompleteAsync()
        {
            return await dbcontext.SaveChangesAsync();
        }
    }
}
