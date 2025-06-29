using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class ItemViewModel
    {
        public string BookName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
