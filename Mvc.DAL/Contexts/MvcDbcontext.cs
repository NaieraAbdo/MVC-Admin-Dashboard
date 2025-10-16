using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DAL.Contexts
{
    public class MvcDbcontext:IdentityDbContext<ApplicationUser>
    {
        public MvcDbcontext(DbContextOptions<MvcDbcontext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("");

        //}

        public  DbSet<Department> DepartmentSet { get; set; }
        public  DbSet<Employee> Employees { get; set; }
    }
}
