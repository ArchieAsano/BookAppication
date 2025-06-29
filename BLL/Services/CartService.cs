using AutoMapper;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddBookToCart(AddToCartDTO addToCartDTO, Guid userid)
        {
            var book = await _unitOfWork.GetRepository<Book>().GetByPropertyAsync(b=>b.Id == addToCartDTO.BookId);
            var cart = await _unitOfWork.GetRepository<Cart>().GetByPropertyAsync(c=>c.UserId == userid);
            var existedDetails = await _unitOfWork.GetRepository<CartDetail>().GetByPropertyAsync(d=>d.CartId == cart.Id && d.BookId == book.Id);
            if (existedDetails != null)
            {
                existedDetails.Quatity += addToCartDTO.Quantity;
                await _unitOfWork.SaveAsync();

            }
            else
            {
                var newcartdetails = new CartDetail()
                {
                    CartId = cart.Id,
                    BookId = addToCartDTO.BookId,
                    Quatity = addToCartDTO.Quantity,
                    Total = book.Price * addToCartDTO.Quantity,
                };
                await _unitOfWork.GetRepository<CartDetail>().AddAsync(newcartdetails);
                await _unitOfWork.SaveAsync();
            }
            
        }

        public async Task CreateUserCart(Guid Userid)
        {
            var cart  = new Cart()
            {
                UserId = Userid,
            };
            await _unitOfWork.GetRepository<Cart>().AddAsync(cart);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CartViewModel> GetUserCart(Guid userid)
        {
            var cart = await _unitOfWork.GetRepository<Cart>().GetByPropertyAsync(c=>c.UserId == userid,includeProperties: "CartDetails.Book");
            if (cart == null) return null;
            var result = _mapper.Map<CartViewModel>(cart);
            return result;
        }

        public async Task RemoveBookFromCart(int bookid, int cartid)
        {          
            await _unitOfWork.GetRepository<CartDetail>().DeleteAsync(cartid,bookid);
            await _unitOfWork.SaveAsync();
        }
    }
}
