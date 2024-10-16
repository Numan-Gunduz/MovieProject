using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MovieProject.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        public int? Rank { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? BigImage { get; set; }
        public string? Thumbnail { get; set; }
        public double? Rating { get; set; }
        public string? Id { get; set; }
        public int? Year { get; set; }
        public string? ImdbId { get; set; }
        public string? ImdbLink { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
