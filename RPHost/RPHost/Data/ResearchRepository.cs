using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.Data
{
    public class ResearchRepository : IResearchRepository
    {
        private readonly DataContext _context;

        public ResearchRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Follow> GetFollow(int userId, int recipientId)
        {
            return await _context.Follows.FirstOrDefaultAsync(u => 
                u.FollowerId == userId && u.FolloweeId == recipientId);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<Photo> GetProfilePhoto(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId)
                    .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

         public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserName == username);
            
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
          
          var users = _context.Users.Include(p => p.Photos)
            .OrderByDescending(u => u.FirstName).AsQueryable();
            //this filters the currently logged in user  
            users = users.Where(u => u.Id != userParams.UserId);

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "lastActive":
                        users = users.OrderBy(u => u.LastActive);
                        break;
                    default:
                        users = users.OrderBy(u => u.FirstName);
                        break;

                }
            }

            if (userParams.Followers)
            {
                var userFollowers = await GetUserFollows(userParams.UserId, userParams.Followers);
                users = users.Where(u => userFollowers.Contains(u.Id));    
            }

            if (userParams.Followees)
            {
                var userFollowees = await GetUserFollows(userParams.UserId, userParams.Followees);
                users = users.Where(u => userFollowees.Contains(u.Id));

            }

         return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize) ;
        }

        private async Task<IEnumerable<int>> GetUserFollows(int id, bool followers)
        {
            var user = await _context.Users
                .Include(x=> x.FollowByUsers)
                .Include(x=> x.FollowedUsers)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (followers)
            {
                return user.FollowByUsers.Where(u => u.FolloweeId == id).Select(i => i.FollowerId);

            }
            else
            {
                return user.FollowedUsers.Where(u => u.FollowerId == id).Select(i => i.FolloweeId);

            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}