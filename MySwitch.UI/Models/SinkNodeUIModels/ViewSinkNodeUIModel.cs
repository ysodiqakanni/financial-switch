using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.Models.SinkNodeModel
{
    public class ViewSinkNodeUIModel : MySwitch.Core.Models.SinkNode
    {
        public List<SinkNode> AllSinkNodes
        {
            get;
            set;
        }
    }
}
