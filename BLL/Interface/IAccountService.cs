using DAL.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAccountService
    {
        Task RegisterAccount(UserRegisterModel userRegisterModel);
        Task<LoginResponseModel> Login (LoginRequestModel loginRequestModel);
        bool VerifyPassword(ApplicationUser user, string inputPassword);
        Task<AccountViewModel>GetAccount (string email);

    }
}
