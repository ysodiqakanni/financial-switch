using MySwitch.Core.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class TransactionDAO : BaseDAO<Transaction>
    {
        public Transaction GetByOriginalDataElement(string originalDataElement)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Transaction>().Where(t => t.OriginalDataElement.Equals(originalDataElement)).SingleOrDefault();
            }
        }
    }
}
