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

        public async Task<Lubie> GetLubie(int userId, int recipientId)
        {
            return await _context.Lajki.FirstOrDefaultAsync(u => 
                u.LubiId == userId && u.LubiiId == recipientId);
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

            if (userParametry.Lubisz)
            {
                var userLajki = await GetUserPolubienia(userParametry.UserId, userParametry.Lubisz);
                users = users.Where(u => userLajki.Contains(u.Id));
            }

            if (userParametry.Lubic)
            {
                var userLajkii = await GetUserPolubienia(userParametry.UserId, userParametry.Lubisz);
                users = users.Where(u => userLajkii.Contains(u.Id));
            }

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

        private async Task<IEnumerable<int>> GetUserPolubienia(int id, bool polubienia)
        {
            var user = await _context.Users
                .Include(x => x.Lubisz)
                .Include(x => x.Lubic)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (polubienia)
            {
                return user.Lubisz.Where(u => u.LubiiId == id).Select(i => i.LubiId);
            }
            else
            {
                return user.Lubic.Where(u => u.LubiId == id).Select(i => i.LubiiId);
            }
        }

        public void Usun<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public  async Task<bool> ZapiszWszystko()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Wiadomosci> GetWiadomosci(int id)
        {
            return await _context.Wiadomosc.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ListaStron<Wiadomosci>> GetWiadomosciDoUser(WiadomosciParametry wiadomosciParametry)
        {
            var wiadomosc = _context.Wiadomosc
                .Include(u => u.Wyslal).ThenInclude(p => p.Zdjecia)
                .Include(u => u.Odbiorca).ThenInclude(p => p.Zdjecia)
                .AsQueryable();

            switch (wiadomosciParametry.NaglowekWiadomosci)
            {
                case "SkrzynkaOdbiorcza":
                    wiadomosc = wiadomosc.Where(u => u.OdbiorcaId == wiadomosciParametry.UserId 
                        && u.OdbiorcaUsunal == false);
                    break;
                case "SkrzynkaNadawcza":
                    wiadomosc = wiadomosc.Where(u => u.WyslalId == wiadomosciParametry.UserId 
                        && u.WysylajacyUsunal == false);
                    break;
                default:
                    wiadomosc = wiadomosc.Where(u => u.OdbiorcaId == wiadomosciParametry.UserId 
                        && u.OdbiorcaUsunal == false && u.JestCzytana == false);
                    break;
            }

            wiadomosc = wiadomosc.OrderByDescending(d => d.DataWyslania);
            return await ListaStron<Wiadomosci>.CreateAsync(wiadomosc, 
                wiadomosciParametry.NumerStrony, wiadomosciParametry.RozmiarStrony);
        }

        public async Task<IEnumerable<Wiadomosci>> GetWiadomosciWatek(int userId, int odbiorcaId)
        {
            var wiadomosc = await _context.Wiadomosc
                .Include(u => u.Wyslal).ThenInclude(p => p.Zdjecia)
                .Include(u => u.Odbiorca).ThenInclude(p => p.Zdjecia)
                .Where(m => m.OdbiorcaId == userId && m.OdbiorcaUsunal == false 
                    && m.WyslalId == odbiorcaId 
                    || m.OdbiorcaId == odbiorcaId && m.WyslalId == userId 
                    && m.WysylajacyUsunal == false)
                .OrderByDescending(m => m.DataWyslania)
                .ToListAsync();

            return wiadomosc;    
        }
    }
}