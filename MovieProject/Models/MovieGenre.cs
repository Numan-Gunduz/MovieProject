namespace MovieProject.Models
{
    public class MovieGenre
    {
        public int MovieId { get; set; } // Film ID
        public Movie Movie { get; set; } // İlişki

        public int GenreId { get; set; } // Tür ID
        public Genre Genre { get; set; } // İlişki
    }
}
