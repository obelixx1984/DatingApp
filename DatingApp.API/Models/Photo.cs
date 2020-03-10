using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Opis { get; set; }
        public DateTime DataDodania { get; set; }
        public bool ToMenu { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}