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

        public async Task<ChatViewModel> GetUserChat(Guid SenderId, Guid ReciverId)
        {
            var chat = await _unitOfWork.GetRepository<Chat>().GetByPropertyAsync(c => (c.Participants1 == SenderId && c.Participants1 == ReciverId)
          || (c.Participants1 == ReciverId && c.Participants2 == SenderId),
                includeProperties: "Messages,Messages.Sender");
            if (chat == null) return null;
            var result = _mapper.Map<ChatViewModel>(chat);
            return result;
        }

        public async Task SendMessage(Guid SenderId, SendMessageModel sendMessageModel)
        {
            var chat = await _unitOfWork.GetRepository<Chat>().GetByPropertyAsync(c => (c.Participants1 == SenderId && c.Participants1 == Guid.Parse(sendMessageModel.ReceiverId))
          || (c.Participants1 == Guid.Parse(sendMessageModel.ReceiverId) && c.Participants2 == SenderId));
            if(chat == null)
            {
                chat = new Chat()
                {
                    Participants1 = SenderId,
                    Participants2 = Guid.Parse(sendMessageModel.ReceiverId),
                };
                await _unitOfWork.GetRepository<Chat>().AddAsync(chat);
                await _unitOfWork.SaveAsync();
            }
            var newmessage = new Message()
            {
                ChatId = chat.Id,
                SenderId = SenderId,
                Content = sendMessageModel.Content,
                CreatedTime = DateTime.UtcNow,
            };
            await _unitOfWork.GetRepository<Message>().AddAsync(newmessage);
            await _unitOfWork.SaveAsync();

        }
    }
}
