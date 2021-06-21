using System.Collections.Generic;
using System.Threading.Tasks;
using RPHost.Helpers;
using RPHost.Models;

namespace RPHost.Data
{
    public interface IResearchRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();

         Task<PagedList<User>> GetUsers(UserParams userParams);

         Task<User> GetUser(int id);
         Task<User> GetUserByUsername(string username);

         Task<Photo> GetPhoto(int id);
         
         Task<Photo> GetProfilePhoto(int userId);

         Task <Follow> GetFollow(int userId, int recipientId);
    }
}