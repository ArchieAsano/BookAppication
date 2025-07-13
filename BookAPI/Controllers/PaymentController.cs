using BLL.Interface;
using DAL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IConfiguration _configuration;
        public PaymentController(IVnPayService vnPayService, IConfiguration configuration)
        {

            _vnPayService = vnPayService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = await _vnPayService.CreatePaymentUrl(model, HttpContext);

            return  Ok(url);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentCallbackVnpay()
        {
            var response =await _vnPayService.PaymentExecute(Request.Query);

            return Ok(response);
        }
        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecute(Request.Query);
            var successUrl = _configuration["Vnpay:SuccessUrl"];
            var errorUrl = _configuration["Vnpay:ErrorUrl"];

            var queryParams = new Dictionary<string, string?>
                {
                    { "vnp_TransactionNo", response.TransactionId },
                    { "vnp_ResponseCode", response.VnPayResponseCode },
                };

            if (response.Success)
            {
              
                var redirectUrl = QueryHelpers.AddQueryString(successUrl, queryParams);
                return Redirect(redirectUrl);
            }
            else
            {
                var redirectUrl = QueryHelpers.AddQueryString(errorUrl, queryParams);
                return Redirect(redirectUrl);
            }

        }
    }
}
