using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.Models.TransactionTypeUIModels
{
    public class ViewTransactionTypeUIModel : TransactionType
    {
        public List<TransactionType> AllTransactionTypes
        {
            get;
            set;
        }
    }
}
