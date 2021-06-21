using System.Linq;
using RPHost.Data;
using HotChocolate;
using HotChocolate.Data;
using RPHost.Models;

namespace RPHost.GraphQL
{
    public class GraphQuery
    {
        // private readonly IMessageRepository _msgRepo;
        // private readonly IMapper _mapper;
        // private readonly IResearchRepository _mainRepo;
        // public GraphQuery(IMessageRepository msgRepo, IMapper mapper,IResearchRepository mainRepo){
        //     _msgRepo = msgRepo;
        //     _mapper = mapper;
        //     _mainRepo = mainRepo;
        // }

        [UseDbContext(typeof(DataContext))]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Message> GetMessage([ScopedService] DataContext context)
        {
            return context.Messages;
        }
    }
}