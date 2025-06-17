using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class LoginResponseModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Role {  get; set; }
        public string Token { get; set; }
    }
}
