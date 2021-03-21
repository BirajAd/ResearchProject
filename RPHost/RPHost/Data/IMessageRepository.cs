using System.Collections.Generic;
using System.Threading.Tasks;
using RPHost.Dtos;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.Data
{
    public interface IMessageRepository
    {
         void AddMessage(Message message);
         void DeleteMessage(Message message);
         Task<Message> GetMessage(int id);
         Task<PagedList<MessageDto>> GetMessageForUser();
         Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId);
         Task<bool> SaveAllAsync();

         
         
    }
}