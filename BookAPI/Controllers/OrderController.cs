using BLL.Interface;
using DAL.DTO;
using DAL.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderSeervice _orderSeService;
        public OrderController(IOrderSeervice orderSeervice)
        {
            _orderSeService = orderSeervice;
        }
        [HttpPost("MakeOrder")]
        public async Task<IActionResult> MakeOrder([FromBody] CreateOrderModel createOrderModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var userid = User.GetUserId();
                createOrderModel.UserId = userid;
                await _orderSeService.MakeOrder(createOrderModel);
                return new OkObjectResult("Make order successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserBill")]
        public async Task<IActionResult> GetUserBill([FromQuery] int billid)
        {
            var bill = await _orderSeService.GetUserOrder(billid);
            if (bill == null) return new NotFoundObjectResult("Not found the information");
            return new OkObjectResult(bill);
        }
    }
}
