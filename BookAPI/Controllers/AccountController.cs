using BLL.Interface;
using DAL.DTO;
using DAL.Library;
using Microsoft.AspNetCore.Authorization;
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

                return Ok(new
                {
                    success = true,
                    message = "Đăng ký thành công!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Đăng ký thất bại: " + ex.Message
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            try
            {
                    var loginresponse = await _accountService.Login(loginRequestModel);
                if (loginresponse == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Sai thông tin đăng nhập!"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Đăng nhập thành công!",
                    data = loginresponse
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Đăng nhập thất bại: " + ex.Message
                });
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

            return Ok(new
            {
                success = true,
                message = "Lấy thông tin người dùng thành công!",
                data = account
            });
        }

        [Authorize]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.GetUserEmail();
            var account = await _accountService.GetAccount(email);

            return Ok(new
            {
                success = true,
                message = "Lấy thông tin người dùng thành công!",
                data = account
            });
        }
    }
}
