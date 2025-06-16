using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class BillDetail
    {
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
