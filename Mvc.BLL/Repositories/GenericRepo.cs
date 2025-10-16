using Microsoft.EntityFrameworkCore;
using Mvc.BLL.Interfaces;
using Mvc.DAL.Contexts;
using Mvc.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.BLL.Repositories
{
    public class GenericRepo<T>:IGenericRepo<T> where T:class
    {
        private readonly MvcDbcontext dbcontext;

        public GenericRepo(MvcDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task AddAsync(T item)
        {
          await  dbcontext.AddAsync(item);
            //return dbcontext.SaveChanges();
        }

        public void Delete(T item)
        {
            dbcontext.Remove(item);
            //return dbcontext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
                return  (IEnumerable<T>) await dbcontext.Employees.Include(e => e.Department).ToListAsync();
            return await dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        => await dbcontext.Set<T>().FindAsync(id);
        

        public void Update(T item)
        {
            dbcontext.Update(item);
            //return dbcontext.SaveChanges();
        }
    }
}
