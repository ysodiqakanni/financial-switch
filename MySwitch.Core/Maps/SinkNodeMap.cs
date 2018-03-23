using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class SinkNodeMap : NodeMap<SinkNode>
    {
       
        public SinkNodeMap()
        {
            //Map(x => x.NodeType).Default(NodeType.Client.ToString());            
        }
         
    }
}
