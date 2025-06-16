using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Bill :BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
