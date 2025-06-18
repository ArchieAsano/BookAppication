using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CartViewModel
    {
        public virtual List<CartDetailViewModel> CartDetails { get; set; }

    }
}
