using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        
        IEnumerable<T> Getall();

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        void Insert(T obj);

        void InsertCollection(IEnumerable<T> list);

        T Update(T obj);

        bool Delete(T obj);

        bool DeleteById(int id);

        void DeleteByCollection(IEnumerable<T> list);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);


    }
}
