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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel userRegisterModel)
        {
            try
            {
                await _accountService.RegisterAccount(userRegisterModel);
                return Created();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginRequestModel loginRequestModel)
        {
            try
            {
                var loginresponse = await _accountService.Login(loginRequestModel);
                if (loginresponse == null) return new NotFoundObjectResult("Wrong Login Information");
                return new OkObjectResult(loginresponse);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetUserAccount")]
        public async Task<IActionResult> GetUserAccount()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var email = User.GetUserEmail();
            var account = await _accountService.GetAccount(email);
            return new OkObjectResult(account);
        }
    }
}
