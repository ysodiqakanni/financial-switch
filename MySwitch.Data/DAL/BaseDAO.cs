using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using System.Linq.Expressions;

namespace MySwitch.Data.DAL
{
    public class BaseDAO<T> : IDAO<T> where T : class
    {
        public void Insert(T t)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction _tranaction = session.BeginTransaction())
                {
                    session.Save(t);
                    _tranaction.Commit();
                }
            }
        }

        public void Update(T t)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction _transaction = session.BeginTransaction())
                {
                    session.Update(t);
                    _transaction.Commit();
                }

            }
        }   //and save changes

        public T GetFirst()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>().ToList().First();
            }
        }
        public T GetById(long Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<T>(Id);
            }
        }
        public int Count()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>().Count();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            using (ISession session = NHibernateHelper.OpenSession())
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
        }

        public List<T> Search(T t, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> filter = null)
        {

            using (var session = NHibernateHelper.OpenSession())
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
}
