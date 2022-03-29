using System.Linq;
using RPHost.Data;
using HotChocolate;
using HotChocolate.Data;
using RPHost.Models;

namespace RPHost.GraphQL
{
    public class GraphQuery
    {

        [UseDbContext(typeof(DataContext))]
        [UseProjection]
        // [UseFiltering]
        // [UseSorting]
        public IQueryable<Message> GetMessage([ScopedService] DataContext context)
        {
            return context.Messages;//.FromSqlInterpolated($@"SELECT Id,SenderId,SenderUsername,RecipientId,RecipientUsername,Content,DateRead,MessageSent,SenderDeleted,RecipientDeleted
                                               // FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY LEAST(senderUsername, recipientUsername), GREATEST(senderUsername, recipientUsername)
                                               // ORDER BY MessageSent DESC) rn FROM Messages WHERE 'mcneil' IN (senderUsername, recipientUsername)) m WHERE rn = 1 ORDER BY MessageSent DESC");
        }

        [UseDbContext(typeof(DataContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUser([ScopedService] DataContext context)
        {
            return context.Users;
        }
    }
}