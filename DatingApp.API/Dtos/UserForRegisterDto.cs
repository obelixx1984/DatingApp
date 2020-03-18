using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Podaj prawidlowe haslo od 4 do 8 znakow")]
        public string Password { get; set; }
       
        [Required]
        public string Plec { get; set; }

        [Required]
        public string Tytul { get; set; }
        
        [Required]
        public DateTime Urodziny { get; set; }
        
        [Required]
        public string Miasto { get; set; }

        [Required]
        public string Kraj { get; set; }
        public DateTime Utworzony { get; set; }
        public DateTime OstatnioAktywny { get; set; }
        
        public UserForRegisterDto()
        {
            Utworzony = DateTime.Now;
            OstatnioAktywny = DateTime.Now;
        }
    }
}