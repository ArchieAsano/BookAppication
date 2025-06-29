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
    public class OrderService : IOrderSeervice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> GetUserOrder(int billid)
        {
            var bill = await _unitOfWork.GetRepository<Bill>().GetByPropertyAsync(b=>b.Id == billid, includeProperties: "User, BillDetails.Book");
            if (bill == null) return null;
            var result = _mapper.Map<OrderViewModel>(bill);
            return result;
        }

        public async Task MakeOrder(CreateOrderModel createOrderModel)
        {
            decimal BillTotal = 0;
            var newbill = new Bill()
            {
                UserId = createOrderModel.UserId,
                CartId = createOrderModel.CartId,
                PaymentMethod = createOrderModel.Paymentmethod,
                CreatedTime = DateTime.UtcNow,
                CreatedBy = createOrderModel.UserId.ToString(),
            };
            await _unitOfWork.GetRepository<Bill>().AddAsync(newbill);
            await _unitOfWork.SaveAsync();
            foreach (var item in createOrderModel.Items)
            {
                var book = await _unitOfWork.GetRepository<Book>().GetByPropertyAsync(b=>b.Id == item.BookId);
                if (book.Quantity == 0 || item.Quantity > book.Quantity) throw new Exception("Invalid Quantity");
                var billdetail = new BillDetail()
                {
                    BillId = newbill.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Total = book.Price * item.Quantity,
                };
                await _unitOfWork.GetRepository<BillDetail>().AddAsync(billdetail);
                BillTotal += billdetail.Total;
                book.Quantity -= item.Quantity;
                await _unitOfWork.SaveAsync();

            }
            var orderedBookIds = createOrderModel.Items.Select(i => i.BookId).ToList();

            var cartDetailsToDelete = await _unitOfWork.GetRepository<CartDetail>()
                .GetAllByPropertyAsync(cd =>
                    cd.CartId == createOrderModel.CartId && orderedBookIds.Contains(cd.BookId)
                );
            foreach (var item in cartDetailsToDelete)
            {
                await _unitOfWork.GetRepository<CartDetail>().DeleteAsync(item.CartId,item.BookId);
                await _unitOfWork.SaveAsync();
            }
            newbill.Total = BillTotal;
            await _unitOfWork.SaveAsync();
        }
    }
}
