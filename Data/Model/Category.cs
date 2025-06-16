using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BookCategory> BookCates { get; set; }

    }
}
