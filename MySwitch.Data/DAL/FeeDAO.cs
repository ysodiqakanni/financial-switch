using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class FeeDAO : BaseDAO<Fee>
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
                    flag = false; //shereshere tinz...   var a = Get(w => w.FlatAmmount == 20, ord => ord.OrderBy(t => t.DateModified)).First();
                }
            }
            return flag;
        }

        public void testFilter()
        {
            var order = Get(null, x => x.OrderByDescending(y => y.Maximum));
            var wher = Get(a => a.Maximum < 50, null);
        }

        public List<Fee> FindFee(Fee fee, int pageIndex, int pageSize, out int totalCount)
        {

            using (var session = NHibernateHelper.OpenSession())
            {
                IList<Fee> listOfFees = null;
                try
                {
                    IList<Fee> feeList = Get() as IList<Fee>;
                    if (!string.IsNullOrEmpty(fee.Name))
                    {
                        listOfFees = feeList.Where(x => x.Name == fee.Name).ToList();
                    }
                    else
                    {
                        listOfFees = feeList;
                    }

                    var result = listOfFees.Skip(pageIndex).Take(pageSize).ToList();
                    totalCount = listOfFees.Count();
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
