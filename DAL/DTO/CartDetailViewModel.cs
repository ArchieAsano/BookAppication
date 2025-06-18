using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CartDetailViewModel
    {
        public string BookName { get; set; }
        public int Quatity { get; set; }
        public decimal Total { get; set; }
    }
}
