using System.Collections.Generic;
using System.Threading.Tasks;
using RPHost.Models;

namespace RPHost.Data
{
    public interface IResearchRepository
    {
         void Add<T>(T entity) where T: class;

         void Delete<T>(T entity) where T: class;

         Task<bool> SaveAll();

         Task<IEnumerable<User>> GetUsers();

         Task<User> GetUser(int id);

         Task<Photo> GetPhoto(int id);
         
         Task<Photo> GetProfilePhoto(int userId);
    }
}