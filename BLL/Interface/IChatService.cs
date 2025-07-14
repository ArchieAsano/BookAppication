using BLL.Services;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IChatService
    {
        Task SendMessage(Guid SenderId, SendMessageModel sendMessageModel);
        Task<ChatViewModel> GetUserChat(Guid SenderId, Guid ReciverId);
    }
}
