using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UserDetaleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Plec { get; set; }
        public int Wiek { get; set; }
        public string Tytul { get; set; }
        public DateTime Utworzony { get; set; }
        public DateTime OstatnioAktywny { get; set; }
        public string Wprowadzony { get; set; }
        public string KogoSzukasz { get; set; }
        public string Zainteresowania { get; set; }
        public string Miasto { get; set; }
        public string Kraj { get; set; }
        public string ZdjecieUrl { get; set; }
        public ICollection<DetaleDlaZdjecDto> Zdjecia { get; set; }
    }
}