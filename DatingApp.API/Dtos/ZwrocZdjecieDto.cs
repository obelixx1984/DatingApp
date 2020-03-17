using System;

namespace DatingApp.API.Dtos
{
    public class ZwrocZdjecieDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Opis { get; set; }
        public DateTime DataDodania { get; set; }
        public bool ToMenu { get; set; }
        public string PubliczneID { get; set; }
    }
}