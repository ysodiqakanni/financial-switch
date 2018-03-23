using MySwitch.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.Models.FeeUIModels
{
    public class ViewFeeUIModel : MySwitch.Core.Models.Fee
    {
        public List<MySwitch.Core.Models.Fee> AllFees
        {
            get;
            set;
        }
    }
}
