using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Plec { get; set; }
        public DateTime Urodziny { get; set; }
        public string Tytul { get; set; }
        public DateTime Utworzony { get; set; }
        public DateTime OstatnioAktywny { get; set; }
        public string Wprowadzony { get; set; }
        public string KogoSzukasz { get; set; }
        public string Zainteresowania { get; set; }
        public string Miasto { get; set; }
        public string Kraj { get; set; }
        public ICollection<Photo> Zdjecia { get; set; }
        public ICollection<Lubie> Lubisz { get; set; }
        public ICollection<Lubie> Lubic { get; set; }

    }
}