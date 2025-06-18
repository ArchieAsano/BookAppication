using BLL.Interface;
using BLL.Services;
using DAL.Interface;
using DAL.Library;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepository();
            services.AddAutoMapper();
            services.AddServices(configuration);
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddMemoryCache();


        }
        public static void AddRepository(this IServiceCollection services)
        {
            services
               .AddScoped<IUnitOfWork,UnitOfWork>();
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ImageUtil>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAccountService,AcconutService>();
            services.AddScoped<ICartService,CartService>();
        }

    }
}
