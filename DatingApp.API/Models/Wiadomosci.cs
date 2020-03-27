using System;

namespace DatingApp.API.Models
{
    public class Wiadomosci
    {
        public int Id { get; set; }
        public int WyslalId { get; set; }
        public User Wyslal { get; set; }
        public int OdbiorcaId { get; set; }
        public User Odbiorca { get; set; }
        public string Tresc { get; set; }
        public bool JestCzytana { get; set; }
        public DateTime? DataCzytania { get; set; }
        public DateTime DataWyslania { get; set; }
        public bool WysylajacyUsunal { get; set; }
        public bool OdbiorcaUsunal { get; set; }
    }
}