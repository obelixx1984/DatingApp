using System;

namespace DatingApp.API.Dtos
{
    public class WiadomoscZwrocDto
    {
        public int Id { get; set; }
        public int WyslalId { get; set; }
        public string WyslalTytul { get; set; }
        public string WyslalZdjecieUrl { get; set; }
        public int OdbiorcaId { get; set; }
        public string OdbiorcaTytul { get; set; }
        public string OdbiorcaZdjecieUrl { get; set; }
        public string Tresc { get; set; }
        public bool JestCzytana { get; set; }
        public DateTime? DataCzytania { get; set; }
        public DateTime DataWyslania { get; set; }
    }
}