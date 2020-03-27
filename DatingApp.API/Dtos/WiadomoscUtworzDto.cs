using System;

namespace DatingApp.API.Dtos
{
    public class WiadomoscUtworzDto
    {
        public int WyslalId { get; set; }
        public int OdbiorcaId { get; set; }
        public DateTime DataWyslania { get; set; }
        public string Tresc { get; set; }
        public WiadomoscUtworzDto()
        {
            DataWyslania = DateTime.Now;
        }
    }
}