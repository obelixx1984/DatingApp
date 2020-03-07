using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required (ErrorMessage = "Pole 'Nazwa użytkownika' jest wymagane.")]
        public string Username { get; set; }
        
        [Required (ErrorMessage = "Pole 'Hasło' jest wymagane.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Podaj prawidlowe haslo od 4 do 8 znakow")]
        public string Password { get; set; }
    }
}