namespace DatingApp.API.Helpers
{
    public class UserParametry
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
        public string Plec { get; set; }
        public int MinWiek { get; set; } = 18;
        public int MaxWiek { get; set; } = 99;
        public string OstatnioByl { get; set; }
    }
}