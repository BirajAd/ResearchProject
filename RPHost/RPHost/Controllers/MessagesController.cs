using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPHost.Data;
using RPHost.Dtos;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.Controllers
{
    // [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IResearchRepository _mainRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IResearchRepository mainRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _mainRepository = mainRepository;
        }

        // [HttpPost("test")]
        // public OkResult TestMe(int userId, UserForLoginDto aDto){
        //     if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //         Console.WriteLine(userId+" Unathorized user. "+int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        //     Console.WriteLine("username: "+aDto.Username);
        //     var res = new Author
        //     {
        //         FirstName = aDto.Username
        //     };
        //     return Ok();
        // }

        [HttpPost]
        public async Task<ActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();
            if(createMessageDto.RecipientUsername == null){
                return BadRequest(username+"has no input."+createMessageDto.RecipientUsername);
            }

            // if (username == createMessageDto.RecipientUsername.ToLower())
            // {
            //     return BadRequest("Message to yourself is not allowed");
            // }

            var sender = await _mainRepository.GetUserByUsername(username);
            var receiver = await _mainRepository.GetUserByUsername(createMessageDto.RecipientUsername);
            Console.WriteLine("name: ", receiver);

            var message = new Message
            {
                Sender = sender,
                Recipient = receiver,
                SenderUsername = sender.Username,
                RecipientUsername = receiver.Username,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<MessageDto>(message));
            }

            return BadRequest("Message not sent.");

        }
    }
}