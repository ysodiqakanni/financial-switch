using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public enum NodeType
    {
        Server, Client
    }
    public class SourceNode : Node
    {

        public virtual IList<Scheme> Schemes { get; set; }
    }
}
