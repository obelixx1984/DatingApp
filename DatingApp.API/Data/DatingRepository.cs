using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Controllers.Data;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Dodaj<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Zdjecia).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Zdjecia).ToListAsync();

            return users;
        }

        public void Usun<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public  async Task<bool> ZapiszWszystko()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}