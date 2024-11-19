//using MovieProject.Models;

//namespace MovieProject.Context
//{
//    public static class SeedData
//    {
//        public static void Initialize(MovieContext context)
//        {
//            // Örnek filmler
//            if (!context.Movies.Any())
//            {
//                context.Movies.AddRange(
//                    new Movie
//                    {
//                        MovieID = 1,
//                        Rank = 1,
//                        Title = "The Shawshank Redemption",
//                        Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
//                        Image = "shawshank.jpg",
//                        BigImage = "shawshank_big.jpg",
//                        Thumbnail = "shawshank_thumb.jpg",
//                        Rating = 9.3,
//                        Year = 1994,
//                        ImdbId = "tt0111161",
//                        ImdbLink = "https://www.imdb.com/title/tt0111161/"
//                    },
//                    new Movie
//                    {
//                        MovieID = 2,
//                        Rank = 2,
//                        Title = "The Godfather",
//                        Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
//                        Image = "godfather.jpg",
//                        BigImage = "godfather_big.jpg",
//                        Thumbnail = "godfather_thumb.jpg",
//                        Rating = 9.2,
//                        Year = 1972,
//                        ImdbId = "tt0068646",
//                        ImdbLink = "https://www.imdb.com/title/tt0068646/"
//                    }
//                );
//            }

//            // Örnek türler
//            if (!context.Genres.Any())
//            {
//                context.Genres.AddRange(
//                    new Genre { GenreId = 1, GenreName = "Drama" },
//                    new Genre { GenreId = 2, GenreName = "Crime" },
//                    new Genre { GenreId = 3, GenreName = "Thriller" }
//                );
//            }

//            // Çoktan çoğa ilişkiler
//            if (!context.MovieGenres.Any())
//            {
//                context.MovieGenres.AddRange(
//                    new MovieGenre { MovieId = 1, GenreId = 1 }, // Shawshank -> Drama
//                    new MovieGenre { MovieId = 2, GenreId = 2 }, // Godfather -> Crime
//                    new MovieGenre { MovieId = 2, GenreId = 3 }  // Godfather -> Thriller
//                );
//            }

//            context.SaveChanges();
//        }
//    }
//}
