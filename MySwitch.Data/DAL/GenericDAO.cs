using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class GenericDAO<T> : IDAO<T> where T : class
    {
        ISession session;
        
        public GenericDAO(ISession session)
        {
            this.session = session;            
        }
        public void Insert(T t)
        {
            session.Save(t);
        }
        public void Update(T t)
        {
            session.Update(t);
        }

        public T GetFirst()
        {
            return session.Query<T>().ToList().First();
        }
        public T GetById(long Id)
        {
            return session.Get<T>(Id);
        }
        public int Count()
        {
            return session.Query<T>().Count();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = session.Query<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList<T>();
            }
        }

        public List<T> Search(T t, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> filter = null)
        {
            try
            {
                var allEntities = session.Query<T>();
                var entitiesFound = allEntities;

                if (filter != null)
                {
                    entitiesFound = allEntities.Where(filter);
                }

                var result = entitiesFound.Skip(pageIndex).Take(pageSize).ToList();
                totalCount = result.Count();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
