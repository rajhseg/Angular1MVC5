using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected IContextFactory factory;

        public Repository(DbContext context, IContextFactory factory)
        {
            this.context = context;
            this.factory = factory;
        }

        public bool Delete(T obj)
        {
            bool result = false;
            try
            {
                context.Set<T>().Remove(obj);
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void DeleteByCollection(IEnumerable<T> list)
        {
            context.Set<T>().RemoveRange(list);
        }

        public bool DeleteById(int id)
        {
            T data = Get(id);
            if (data != null)
                return Delete(data);
            return false;
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> Getall()
        {
            return context.Set<T>().ToList();
        }

        public void Insert(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void InsertCollection(IEnumerable<T> list)
        {
            context.Set<T>().AddRange(list);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().SingleOrDefault(predicate);
        }

        public T Update(T obj)
        {
            context.Entry<T>(obj).State = EntityState.Modified;
            return obj;
        }        

        public PurchaseDBContext Context
        {
            get
            {
                return this.factory.GetContext<PurchaseDBContext>(this.context);
            }
        }

    }
}
