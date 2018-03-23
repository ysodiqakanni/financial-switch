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
    public class RouteDAO : BaseDAO<Route>
    {
        public bool isUniqueName(string name)
        {
            bool flag = true;
            if (Get().Any(n => n.Name.ToLower().Equals(name.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueName(string oldName, string newName)
        {
            bool flag = true;
            if (!oldName.ToLower().Equals(newName.ToLower()))
            {
                if (Get().Any(n => n.Name.ToLower().Equals(newName.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public bool isUniqueBIN (string bin)
        {
            bool flag = true;
            if (Get().Any(n => n.BIN == bin))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueBIN(string oldBin, string newBin)
        {
            bool flag = true;
            if (!(oldBin == newBin))
            {
                if (Get().Any(n => n.BIN == newBin))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public Route GetByCardBIN(string bin)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<Route>().Where(r => r.BIN.Substring(0, 6) == bin.Substring(0, 6)).SingleOrDefault();
            }
        }
    }
}
