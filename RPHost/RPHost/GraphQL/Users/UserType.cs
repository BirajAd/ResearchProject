using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using RPHost.Data;
using RPHost.Models;

namespace RPHost.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Description("represents all users");

            descriptor.Authorize();

            descriptor
                .Field(u => u.PasswordHash).Ignore();

            descriptor
                .Field(u => u.SecurityStamp).Ignore();

            descriptor
                .Field(u => u.ConcurrencyStamp).Ignore();

            descriptor
                .Field(u => u.NormalizedUserName).Ignore();

            descriptor
                .Field(u => u.NormalizedEmail).Ignore();

            descriptor
                .Field(u => u.TwoFactorEnabled).Ignore();
            
            descriptor
                .Field(u => u.LockoutEnd).Ignore();

            descriptor
                .Field(u => u.LockoutEnabled).Ignore();

            descriptor
                .Field(u => u.AccessFailedCount).Ignore();

            descriptor
                .Field(u => u.FollowByUsers).Ignore();
            
            descriptor
                .Field(u => u.FollowedUsers).Ignore();

            descriptor
                .Field(u => u.UserRoles).Ignore();

            // descriptor
            //     .Field(u => u.Photos)
            //     .ResolveWith<Resolvers>(u => u.GetPhotos(default!, default!))
            //     .UseDbContext<DataContext>()
            //     .Description("List of photos");

            // descriptor
            //     .Field(u => u.MessagesReceived)
            //     .ResolveWith<Resolvers>(u => u.SentMessages(default!, default!))
            //     .UseDbContext<DataContext>()
            //     .Description("sent messages");

            // descriptor
            //     .Field(u => u.MessagesReceived)
            //     .ResolveWith<Resolvers>(u => u.ReceivedMessages(default!, default!))
            //     .UseDbContext<DataContext>()
            //     .Description("received messages");
        }

        private class Resolvers
        {
            // public IQueryable<Photo> GetPhotos(User user, [ScopedService] DataContext context)
            // {
            //     return context.Photos.Where(p => p.UserId == user.Id);
            // }

            // public IQueryable<Message> SentMessages(User user, [ScopedService] DataContext context)
            // {
            //     return context.Messages.Where(m => m.Sender.Id == user.Id);
            // }

            // public IQueryable<Message> ReceivedMessages(User user, [ScopedService] DataContext context)
            // {
            //     return context.Messages.Where(m => m.Recipient.Id == user.Id);
            }

            public IQueryable<Message> AllMessages(User user, [ScopedService] DataContext context)
            {
                return context.Messages.Where(m => m.Recipient.Id == user.Id && m.Sender.Id == user.Id);
            }
        }
}