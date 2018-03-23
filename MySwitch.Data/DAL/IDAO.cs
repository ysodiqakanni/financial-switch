using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MySwitch.Data.DAL
{
    public interface IDAO<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        void Insert(T entity);
        void Update(T entity);
        T GetById(long id);
        int Count();
    }
}
