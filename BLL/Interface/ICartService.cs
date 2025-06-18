using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICartService
    {
        Task CreateUserCart(Guid Userid);
        Task AddBookToCart(AddToCartDTO addToCartDTO, Guid userid); 
        Task<CartViewModel> GetUserCart(Guid userid);
        
    }
}
