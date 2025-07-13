using DAL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IVnPayService
    {
         Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        Task <PaymentResponseModel> PaymentExecute(IQueryCollection collections);

    }
}
