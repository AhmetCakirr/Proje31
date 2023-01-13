namespace Proje31.Models
{
    public class faturaListele
    {
        public int id { get; set; }
        public string aboneNo { get; set; }
        public  string  adsoyad { get; set; }
        public  string adres { get; set; }
        public DateTime sonodemetarihi { get; set; }
        public  float   tutar { get; set; }
        public bool isActive { get; set; }
        public DateTime gelistarihi { get; set; }
    }
}
