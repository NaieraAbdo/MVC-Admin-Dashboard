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

        public void Add(T item)
        {
            dbcontext.Add(item);
            //return dbcontext.SaveChanges();
        }

        public void Delete(T item)
        {
            dbcontext.Remove(item);
            //return dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>) dbcontext.Employees.Include(e => e.Department).ToList();
            return dbcontext.Set<T>().ToList();
        }

        public T GetById(int id)
        => dbcontext.Set<T>().Find(id);
        

        public void Update(T item)
        {
            dbcontext.Update(item);
            //return dbcontext.SaveChanges();
        }
    }
}
