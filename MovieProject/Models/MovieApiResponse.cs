namespace MovieProject.Models
{

        public class MovieApiResponse
        {
            public int Rank { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public string BigImage { get; set; }
        public List<string> Genre { get; set; }
        public string Thumbnail { get; set; }
            public double Rating { get; set; }
            public string Id { get; set; }
            public int Year { get; set; }
            public string ImdbId { get; set; }
            public string ImdbLink { get; set; }
        }
    
}
