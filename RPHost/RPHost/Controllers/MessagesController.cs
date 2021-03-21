using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPHost.Data;
using RPHost.Dtos;

namespace RPHost.Controllers
{
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IResearchRepository _mainRepository;
        private readonly IMessageRepository _messageRepository;
        public MessagesController(IResearchRepository mainRepository, IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _mainRepository = mainRepository;
        }

        // public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        // {
        //     var username = User.GetUserName();
        // }
    }
}