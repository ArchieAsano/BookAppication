﻿using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class ChatViewModel
    {
        public virtual List<MessageViewModel> Messages { get; set; }
    }
}
