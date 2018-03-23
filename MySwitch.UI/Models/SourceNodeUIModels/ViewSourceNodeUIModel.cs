using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySwitch.Data.DAL;

namespace MySwitch.UI.Models.SourceNodeUIModels
{
    public class ViewSourceNodeUIModel : MySwitch.Core.Models.SourceNode
    {
        public List<MySwitch.Core.Models.SourceNode> AllSourceNodes
        {
            get;
            set;
        }
    }
}
