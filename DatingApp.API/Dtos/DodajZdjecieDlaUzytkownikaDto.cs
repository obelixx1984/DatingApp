using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class DodajZdjecieDlaUzytkownikaDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Opis { get; set; }
        public DateTime DataDodania { get; set; }
        public string PubliczneID { get; set; }
        public DodajZdjecieDlaUzytkownikaDto()
        {
            DataDodania = DateTime.Now;
        }
    }
}