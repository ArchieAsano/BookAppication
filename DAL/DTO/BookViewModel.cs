using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string CoverImg { get; set; }
        public string PreContent { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<BookCategoryViewModel> BookCates { get; set; }
    }
}
