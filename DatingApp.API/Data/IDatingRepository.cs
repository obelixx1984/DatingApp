using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
         void Dodaj<T>(T entity) where T: class;
         void Usun<T>(T entity) where T: class;
         Task<bool> ZapiszWszystko();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}