using System;
using System.Collections.Generic;
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
    [ApiController]
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

        [HttpPost]
        public async Task<ActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();
            if(createMessageDto.RecipientUsername == null){
                return BadRequest(username+" has no input. "+createMessageDto.RecipientUsername);
            }

            if (username == createMessageDto.RecipientUsername.ToLower())
            {
                return BadRequest("Message to yourself is not allowed");
            }

            var sender = await _mainRepository.GetUserByUsername(username);
            var receiver = await _mainRepository.GetUserByUsername(createMessageDto.RecipientUsername);

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

        [HttpGet]
        public async Task<ActionResult> GetMessageForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var messages = await _messageRepository.GetMessageForUser(messageParams);
            Response.AddPagination(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return Ok(messages);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();

            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }
    }
}