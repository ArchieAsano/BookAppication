using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Cart :BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }
        //public ICollection<Bill> Bills { get; set; }
    }
}
