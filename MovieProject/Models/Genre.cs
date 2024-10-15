namespace MovieProject.Models
{
    public class Genre
    {
        public int GenreId { get; set; }             // Tür ID (Primary Key)
        public string GenreName { get; set; }         // Tür adı (örneğin: Drama, Komedi)

        // İlişki: Bir tür birden fazla filmde olabilir
        public ICollection<Movie> Movies { get; set; } // Tür ile Filmler ilişkisi
    }
}
