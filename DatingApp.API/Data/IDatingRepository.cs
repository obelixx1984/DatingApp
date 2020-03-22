using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
         void Dodaj<T>(T entity) where T: class;
         void Usun<T>(T entity) where T: class;
         Task<bool> ZapiszWszystko();
         Task<ListaStron<User>> GetUsers(UserParametry userParametry);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
         Task<Lubie> GetLubie(int userId, int recipientId);
    }
}