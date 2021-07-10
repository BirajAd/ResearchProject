using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RPHost.Data;
using RPHost.Dtos;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IResearchRepository _mainRepository;
        public MessageHub(IMessageRepository messageRepository, IResearchRepository mainRepository, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages = await _messageRepository.GetMessageThread(Context.User
                        .GetUsername(), otherUser);

            await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
            {
                throw new HubException("you cannot send message to yourself");
            }

            var sender = await _mainRepository.GetUserByUsername(username);
            var receiver = await _mainRepository.GetUserByUsername(createMessageDto.RecipientUsername);

            if(receiver == null) throw new HubException("User not found");

            var message = new Message
            {
                Sender = sender,
                Recipient = receiver,
                SenderUsername = sender.UserName,
                RecipientUsername = receiver.UserName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
            {
                var group = GetGroupName(sender.UserName, receiver.UserName);
                await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}