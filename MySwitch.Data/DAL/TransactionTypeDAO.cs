using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class TransactionTypeDAO : BaseDAO<TransactionType>
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
        public bool isUniqueCode(string code)
        {
            bool flag = true;
            if (Get().Any(n => n.Code.ToLower().Equals(code.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueCode(string oldCode, string newCode)
        {
            bool flag = true;
            if (!oldCode.ToLower().Equals(newCode.ToLower()))
            {
                if (Get().Any(n => n.Code.ToLower().Equals(newCode.ToLower())))
                {
                    flag = false;
                }
            }            
            return flag;
        }

       
    }
}
