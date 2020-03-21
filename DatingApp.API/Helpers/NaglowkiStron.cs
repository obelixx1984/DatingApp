namespace DatingApp.API.Helpers
{
    public class NaglowkiStron
    {
        public int DomyslnaStrona { get; set; }
        public int ItemsNaStrone { get; set; }
        public int CaloscItems { get; set; }
        public int CaloscStron { get; set; }
        public NaglowkiStron(int domyslnaStrona, int itemsNaStrone, int caloscItems, int caloscStron)
        {
            this.DomyslnaStrona = domyslnaStrona;
            this.ItemsNaStrone = itemsNaStrone;
            this.CaloscItems = caloscItems;
            this.CaloscStron = caloscStron;
        }
    }
}