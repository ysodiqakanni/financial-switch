using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.Models.TransactionLogUI
{
    public class ViewTransactionLogUIModel : Transaction
    {
        public List<Transaction> AllTransactions 
        {
            get;
            set;
        }

    }
}
