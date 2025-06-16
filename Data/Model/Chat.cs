using Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Chat : BaseEntity
    {
        public Guid Participants1 { get; set; }
        public virtual ApplicationUser User1 { get; set; }

        public Guid Participants2 { get; set; }
        public virtual ApplicationUser User2 { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
