namespace DatingApp.API.Helpers
{
    public class WiadomosciParametry
    {
        private const int MaxRozmiarStron = 50;
        public int NumerStrony { get; set; } = 1;
        private int rozmiarStrony = 10;
        public int RozmiarStrony
        {
            get { return rozmiarStrony; }
            set { rozmiarStrony = (value > MaxRozmiarStron) ? MaxRozmiarStron : value; }
        }
 
        public int UserId { get; set; }
        public string NaglowekWiadomosci { get; set; } = "Nieprzeczytane";

    }
}