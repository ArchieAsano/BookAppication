using AutoMapper;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChatViewModel> GetUserChat(Guid UserId)
        {
            var chat = await _unitOfWork.GetRepository<Chat>().GetByPropertyAsync(
                c=>c.Participants1 == UserId || c.Participants2 == UserId,
                includeProperties: "Messages.Sender");
            if (chat == null) return null;
            var result = _mapper.Map<ChatViewModel>(chat);
            return result;
        }

        public async Task SendMessage(Guid SenderId, Guid ReceiverId, string message)
        {
            var chat = await _unitOfWork.GetRepository<Chat>().GetByPropertyAsync(c => (c.Participants1 == SenderId && c.Participants1 == ReceiverId)
          || (c.Participants1 == ReceiverId && c.Participants2 == SenderId));
            if(chat == null)
            {
                chat = new Chat()
                {
                    Participants1 = SenderId,
                    Participants2 = ReceiverId,
                };
                await _unitOfWork.GetRepository<Chat>().AddAsync(chat);
                await _unitOfWork.SaveAsync();
            }
            var newmessage = new Message()
            {
                ChatId = chat.Id,
                SenderId = SenderId,
                Content = message,
                CreatedTime = DateTime.UtcNow,
            };
            await _unitOfWork.GetRepository<Message>().AddAsync(newmessage);
            await _unitOfWork.SaveAsync();

        }
    }
}
