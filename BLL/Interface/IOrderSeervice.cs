using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IOrderSeervice
    {
        Task MakeOrder (CreateOrderModel createOrderModel);
        Task<OrderViewModel> GetUserOrder(int billid);
    }
}
