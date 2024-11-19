using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        public string GenreName { get; set; }

        // Bir türün birden fazla filme ait olabileceğini belirtmek için
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
