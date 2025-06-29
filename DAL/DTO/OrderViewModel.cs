using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class OrderViewModel
    {
        public string UserName { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }

        public virtual List<ItemViewModel> BillDetails { get; set; }
    }
}
