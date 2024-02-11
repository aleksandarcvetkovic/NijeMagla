namespace Nije_Magla_API
{
    public class PolutionListItem
    {
        public string Ime { get; set; }
        public string Lokacija { get; set; }

        public int Vrednost { get; set; }

        public PolutionListItem(string ime, string lokacija, int vrednost)
        {
            Ime = ime;
            Lokacija = lokacija;
            Vrednost = vrednost;
        }
    }
}
