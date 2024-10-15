namespace MovieProject.Models
{
    public class Genre
    {
        public int GenreId { get; set; } // Tür ID (Primary Key)
        public string GenreName { get; set; } // Tür adı

        // İlişki: Bir tür birden fazla filmde olabilir
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
