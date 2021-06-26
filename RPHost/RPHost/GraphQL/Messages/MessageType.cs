using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using RPHost.Data;
using RPHost.Models;

namespace RPHost.GraphQL.Messages
{
    public class MessageType : ObjectType<Message>
    {
        protected override void Configure(IObjectTypeDescriptor<Message> descriptor)
        {
            descriptor.Description("Represents message for all users");

            descriptor.Authorize();

            descriptor
                .Field(m => m.Sender)
                .ResolveWith<Resolvers>(m => m.GetSenders(default!, default!))
                .UseDbContext<DataContext>()
                .Description("List of message senders");

            descriptor
                .Field(m => m.Recipient)
                .ResolveWith<Resolvers>(m => m.GetRecipients(default!, default!))
                .UseDbContext<DataContext>()
                .Description("List of message recipients");
        }

        private class Resolvers
        {
            public IQueryable<User> GetSenders(Message msg, [ScopedService] DataContext context)
            {
                return context.Users.Where(u => u.Id == msg.SenderId);
            }

            public IQueryable<User> GetRecipients(Message msg, [ScopedService] DataContext context)
            {
                return context.Users.Where(u => u.Id == msg.RecipientId);
            }
        }
    }
}