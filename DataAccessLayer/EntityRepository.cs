using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> 
    where T : class, IDomainObject
    {
        public Context context;

        public EntityRepository()
        {
            context = new Context();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetItem(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }   

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(GetItem(id));
            context.SaveChanges();
        }
    }
}
