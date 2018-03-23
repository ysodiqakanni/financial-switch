using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class NodeMap<T> : EntityMap<T> where T : Node
    {
        
        public NodeMap()
        {
            Map(x => x.Name);
            Map(x => x.HostName);
            Map(x => x.IpAddress);
            Map(x => x.Port);
            Map(x => x.Status);
            Map(x => x.NodeType);
        }
         
    }
}
