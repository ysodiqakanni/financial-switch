using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.Core.Models
{
    public class Combo : Entity
    {
        public virtual string Name { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Fee Fee { get; set; }
    }
}
