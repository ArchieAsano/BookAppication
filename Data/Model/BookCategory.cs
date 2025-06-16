using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int CateId { get; set; }
        public virtual Category Category { get; set; }
    }
}
