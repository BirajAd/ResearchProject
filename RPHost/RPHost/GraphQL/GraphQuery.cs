using System.Linq;
using RPHost.Models;
using RPHost.Data;
using HotChocolate;

namespace RPHost.GraphQL
{
    public class GraphQuery
    {
        // [UseDbContext(typeof(DataContext))]
        public IQueryable<Message> GetMessage([Service] DataContext context)
        {
            return context.Messages;
        }
    }
}