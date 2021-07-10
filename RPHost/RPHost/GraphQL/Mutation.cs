using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPHost.Data;
using RPHost.GraphQL.Messages;
using RPHost.Models;
using System.Security;
using System.Security.Claims;
using RPHost.Helpers;
using System;
using HotChocolate.Types;
using System.Linq;

namespace RPHost.GraphQL
{
    public class Mutation
    {
        private readonly IResearchRepository _mainRepository;
        private readonly IHttpContextAccessor _cont;

        public Mutation([Service] IResearchRepository mainRepository, [Service] IHttpContextAccessor cont)
        {
            _mainRepository = mainRepository;
            _cont = cont;
        }

        // [UseDbContext(typeof(DataContext))]
        public async Task<AddMessagePayload> AddMessageAsync(AddMessageInput input)//, [ScopedService] DataContext context)
        {
            var username = _cont.HttpContext.User.GetUsername();
            Console.WriteLine("Inititated");
            var sender = await _mainRepository.GetUserByUsername(username);
            Console.WriteLine("\n\n Username: ");
            var receiver = await _mainRepository.GetUserByUsername(input.RecipientUsername);
            var message = new Message
            {
                Sender = sender, 
                Recipient = receiver,
                SenderUsername = username, 
                RecipientUsername = input.RecipientUsername,
                Content = input.Content 
            };
            Console.WriteLine("Message created.\n\n"+message.Sender.UserName+" to "+message.Recipient.UserName+" => "+message.MessageSent);
            _mainRepository.Add<Message>(message);
            Console.WriteLine("Message added.");
            await _mainRepository.SaveAll();

            return new AddMessagePayload(message);

        }
    }
}