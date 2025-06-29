using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CreateOrderModel
    {
        public Guid UserId { get; set; }
        public int CartId { get; set; }
        public string Paymentmethod {  get; set; }
        public List<OrderItemModel> Items { get; set; }

    }
}
