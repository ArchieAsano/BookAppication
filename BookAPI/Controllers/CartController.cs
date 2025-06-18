using BLL.Interface;
using DAL.DTO;
using DAL.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        [HttpPost("AddBookToCart")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddToCartDTO addToCartDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userid = User.GetUserId();
            await _cartService.AddBookToCart(addToCartDTO, userid);
            return new OkObjectResult("Add to cart successfully");
        }
        [Authorize]
        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCart()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userid = User.GetUserId();
            var cart =await _cartService.GetUserCart(userid);
            if (cart == null) return new NotFoundObjectResult("Can not find cart");
            return new OkObjectResult(cart);

        }
        [Authorize]
        [HttpDelete("RemoveCartDetail")]
        public async Task<IActionResult> RemoveCartDetail([FromQuery]int bookid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var userid = User.GetUserId();
                var cart = await _cartService.GetUserCart(userid);
                await _cartService.RemoveBookFromCart(bookid, cart.Id);
                return new OkObjectResult("Remove cart detail");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
