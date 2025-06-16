using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int Quatity { get; set; }
        public decimal Total { get; set; }
    }
}
