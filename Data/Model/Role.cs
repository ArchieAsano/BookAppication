using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Role: BaseEntity
    {
        string Name { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

    }
}
