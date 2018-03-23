using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class ComboDAO : BaseDAO<Combo>
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
    }
}
