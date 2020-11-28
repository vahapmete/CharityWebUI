using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CharityWebUI.DataAccess.IMainRepository
{
    public interface IRepository<T> where T: class
    {
        T Get(int id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null,
            string includeProperties=null);

        T GetFirsOrDefault(Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        void Add(T entity);
        //void Update(T entity);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRang(IEnumerable<T> entity);
    }
}
