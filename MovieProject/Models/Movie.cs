namespace MovieProject.Models
{
    public class Movie
    {
        public int MovieId { get; set; }             // Film ID
        public string MovieName { get; set; }           // Film adı
        public string Director { get; set; }     // Yönetmen
        public double Metascore { get; set; }       // Metascore değeri
        public DateTime ReleaseDate { get; set; } // Yayın tarihi
                                                  // İlişki: Bir film birden fazla türle ilişkilendirilebilir
        public ICollection<Genre> Genres { get; set; } // Film ile Tür ilişkisi

    }
}
