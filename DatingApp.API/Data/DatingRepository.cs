using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Controllers.Data;
using DatingApp.API.Helpers;
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

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Zdjecia.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.ToMenu);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Zdjecia.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Zdjecia).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<ListaStron<User>> GetUsers(UserParametry userParametry)
        {
            var users = _context.Users.Include(p => p.Zdjecia)
                .OrderByDescending(u => u.OstatnioAktywny).AsQueryable();

            users = users.Where(u => u.Id != userParametry.UserId);

            users = users.Where(u => u.Plec == userParametry.Plec);

            if (userParametry.MinWiek != 18 || userParametry.MaxWiek != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParametry.MaxWiek - 1);
                var maxDob = DateTime.Today.AddYears(-userParametry.MinWiek);

                users = users.Where(u => u.Urodziny >= minDob && u.Urodziny <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParametry.OstatnioByl))
            {
                switch (userParametry.OstatnioByl)
                {
                    case "utworzony":
                        users = users.OrderByDescending(u => u.Utworzony);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.OstatnioAktywny); 
                        break;
                }
            }

            return await ListaStron<User>.CreateAsync(users, userParametry.NumerStrony, userParametry.RozmiarStrony);
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