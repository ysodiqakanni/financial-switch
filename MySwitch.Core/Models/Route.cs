using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public class Route : Entity
    {
        public virtual string Name { get; set; }
        public virtual string BIN { get; set; }
        public virtual string Description { get; set; }
        public virtual SinkNode SinkNode { get; set; }
    }
}
