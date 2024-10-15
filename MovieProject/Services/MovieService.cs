using Microsoft.EntityFrameworkCore;
using MovieProject.Context;
using MovieProject.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MovieProject.Services
{
    public class MovieService
    {
        private readonly MovieContext _context;

        public MovieService(MovieContext context)
        {
            _context = context;
        }

        public async Task FetchAndSaveMoviesAsync()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                    Headers =
                    {
                        { "x-rapidapi-key", "1d334c7fe2msh2159895426a82ccp1305a3jsn1aea55313e21" },
                        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var movieApiResponses = JsonConvert.DeserializeObject<List<MovieApiResponse>>(body);

                    // Veritabanına kaydet
                    foreach (var movieApiResponse in movieApiResponses)
                    {
                        // Mevcut film olup olmadığını kontrol edin
                        var existingMovie = await _context.Movies
                            .Include(m => m.Genres)
                            .FirstOrDefaultAsync(m => m.ImdbId == movieApiResponse.imdbid);

                        if (existingMovie != null)
                        {
                            // Film zaten mevcutsa güncelle
                            existingMovie.Rank = movieApiResponse.rank;
                            existingMovie.Title = movieApiResponse.title;
                            existingMovie.Description = movieApiResponse.description;
                            existingMovie.Image = movieApiResponse.image;
                            existingMovie.BigImage = movieApiResponse.big_image;
                            existingMovie.Thumbnail = movieApiResponse.thumbnail;
                            existingMovie.Rating = movieApiResponse.rating;
                            existingMovie.Year = movieApiResponse.year;
                            existingMovie.ImdbLink = movieApiResponse.imdb_link;

                            // Türleri güncelle
                            existingMovie.Genres.Clear();
                            if (movieApiResponse.genre != null)
                            {
                                foreach (var genreName in movieApiResponse.genre)
                                {
                                    var genre = await _context.Genres
                                        .FirstOrDefaultAsync(g => g.GenreName == genreName)
                                        ?? new Genre { GenreName = genreName };

                                    existingMovie.Genres.Add(genre);
                                }
                            }
                        }
                        else
                        {
                            // Film mevcut değilse yeni bir kayıt oluştur
                            var movie = new Movie
                            {
                                Rank = movieApiResponse.rank,
                                Title = movieApiResponse.title,
                                Description = movieApiResponse.description,
                                Image = movieApiResponse.image,
                                BigImage = movieApiResponse.big_image,
                                Thumbnail = movieApiResponse.thumbnail,
                                Rating = movieApiResponse.rating,
                                Year = movieApiResponse.year,
                                ImdbId = movieApiResponse.imdbid,
                                ImdbLink = movieApiResponse.imdb_link,
                                Genres = new List<Genre>() // Türler burada eklenebilir
                            };

                            // Türleri ekleyin
                            if (movieApiResponse.genre != null)
                            {
                                foreach (var genreName in movieApiResponse.genre)
                                {
                                    var genre = await _context.Genres
                                        .FirstOrDefaultAsync(g => g.GenreName == genreName)
                                        ?? new Genre { GenreName = genreName };

                                    movie.Genres.Add(genre);
                                }
                            }

                            _context.Movies.Add(movie);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }

    public class MovieApiResponse
    {
        public int rank { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string big_image { get; set; }
        public List<string> genre { get; set; }
        public string thumbnail { get; set; }
        public double rating { get; set; }
        public int year { get; set; }
        public string imdbid { get; set; }
        public string imdb_link { get; set; }
    }
}
