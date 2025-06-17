using AutoMapper;
using Azure.Core;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;
using DAL.Library;
using Data.Base;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AcconutService : IAccountService
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageUtil _imageUtil;
        private readonly JWTSettings _jwtSettings;

        public AcconutService(IUnitOfWork unitOfWork, IMapper mapper,ImageUtil imageUtil,IOptions<JWTSettings> options) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageUtil = imageUtil;
            _jwtSettings = options.Value;
        }

        public async Task<AccountViewModel> GetAccount(string email)
        {
            var account = await _unitOfWork.GetRepository<ApplicationUser>().GetByPropertyAsync(a => a.Email == email);
            var result = _mapper.Map<AccountViewModel>(account);
            return result;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel)
        {
            var account = await _unitOfWork.GetRepository<ApplicationUser>().GetByPropertyAsync(a=>a.Email == loginRequestModel.Email);
            var checkpassword = VerifyPassword(account, loginRequestModel.Password);
            if (account == null || checkpassword == false) return null;           
            LoginResponseModel tokenrs = await Authentication.CreateToken(account!, account.RoleId!, _jwtSettings);
            return tokenrs;
        }

        public async Task RegisterAccount(UserRegisterModel userRegisterModel)
        {
            var user = _mapper.Map<ApplicationUser>(userRegisterModel);
            if(userRegisterModel.AvatarImg != null)
            {
                var imgurl = await _imageUtil.UploadImageAsync(userRegisterModel.AvatarImg);
                user.ImgUrl = imgurl;
            }
            string hashedPassword = _passwordHasher.HashPassword(user, userRegisterModel.Password);
            user.PasswordHash = hashedPassword;
            await _unitOfWork.GetRepository<ApplicationUser>().AddAsync(user);
            await _unitOfWork.SaveAsync();
        }
        public bool VerifyPassword(ApplicationUser user, string inputPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
