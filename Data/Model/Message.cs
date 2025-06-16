using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Message :BaseEntity
    {
        public string Content { get; set; }

        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        public Guid SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}
