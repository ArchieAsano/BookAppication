using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
