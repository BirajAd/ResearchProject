using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RPHost.Dtos;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<PagedList<MessageDto>> GetMessageForUser(MessageParams messageParams)
        {
            var query = _context.Messages.
                OrderByDescending(m => m.MessageSent)
                // .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var user = messageParams.Username;

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username),
                "Unread" => query.Where(u => u.RecipientUsername == messageParams.Username && u.DateRead == null),
                _ => _context.Messages.FromSqlInterpolated($@"SELECT Id,SenderId,SenderUsername,RecipientId,RecipientUsername,Content,DateRead,MessageSent,SenderDeleted,RecipientDeleted
                                                FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY LEAST(senderUsername, recipientUsername), GREATEST(senderUsername, recipientUsername)
                                                ORDER BY MessageSent DESC) rn FROM Messages WHERE {user} IN (senderUsername, recipientUsername)) m WHERE rn = 1 ORDER BY MessageSent DESC")
                                                // .ProjectTo<MessageDto>(_mapper.ConfigurationProvider).AsQueryable()
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.Recipient.UserName == currentUsername
                && m.Sender.UserName == recipientUsername && m.RecipientDeleted == false
                || m.Sender.UserName == currentUsername
                && m.Recipient.UserName == recipientUsername && m.SenderDeleted == false)
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            var unreadMessages = messages.Where(m => m.RecipientUsername == currentUsername && m.DateRead == null).ToList();

            if(unreadMessages.Any())
            {
                foreach(var msg in unreadMessages)
                {
                    msg.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return messages;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}