using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Book :BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string CoverImg { get; set; }
        public string PreContent { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<BookCategory> BookCates { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
