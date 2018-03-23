using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public class Node : Entity
    {
        public virtual string Name { get; set; }
        public virtual string HostName { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual int Port { get; set; }
        public virtual NodeStatus Status { get; set; }
        public virtual NodeType NodeType { get; set; }
    }
}
