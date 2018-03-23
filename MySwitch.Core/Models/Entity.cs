using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public class Entity
    {
        public virtual long ID { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }

        
        /// <summary>
        /// overriding this Equals() makes the Route of a scheme Preselected upon edit (same in any other scenario). 
        /// This is because the  default route passed to the edit page is compared with those passed to the dropdownlist
        /// in the edit page. since the proces of fetching the preselected route has a different NHibernate session from
        /// that of fetching all routes from the db to the dropdownlist, the equality will be false and the item won't be
        /// selected. So this override is done to ensure the equality is checked using the ID and not the session
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public override bool Equals(object obj)     
        {
            if (obj != null && obj is Entity)
            {
                return (obj as Entity).ID == this.ID;
            }
            return base.Equals(obj);
        }
    }
   
}
