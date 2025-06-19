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
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage( [FromBody] SendMessageModel Message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var senderId = User.GetUserId();
            try
            {
                await _chatService.SendMessage(senderId, Message);
                return new OkObjectResult("Send successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUserChat")]
        public async Task<IActionResult> GetUserChat()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var senderId = User.GetUserId();
            var chat = await _chatService.GetUserChat(senderId);
            if (chat == null) return new NotFoundObjectResult("Can not found chat");
            return new OkObjectResult(chat);
        }
    }
}
