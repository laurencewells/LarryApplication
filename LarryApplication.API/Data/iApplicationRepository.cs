using System.Collections.Generic;
using System.Threading.Tasks;
using LarryApplication.API.Models;

namespace LarryApplication.API.Data
{
    public interface iApplicationRepository
    {
         void add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);


    }
}