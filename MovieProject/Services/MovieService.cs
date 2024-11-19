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

        // Tüm filmleri getir
        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies.Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).ToList();
        }
        public Dictionary<string, List<Movie>> GetMoviesByGenres()
        {
            // Tüm türleri ve ilişkili filmleri al
            var genresWithMovies = _context.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(mg => mg.Movie)
                .ToList();

            // Filmleri türlerine göre gruplandır ve dictionary yapısında döndür
            var result = new Dictionary<string, List<Movie>>();

            foreach (var genre in genresWithMovies)
            {
                result[genre.GenreName] = genre.MovieGenres.Select(mg => mg.Movie).ToList();
            }

            return result;
        }

        //Filmleri API'den çekip veritabanına kaydet
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

                    // Veritabanına kaydetme işlemi
                    foreach (var movieApiResponse in movieApiResponses)
                    {
                        // IMDb ID'sine göre mevcut film kontrolü
                        var existingMovie = await _context.Movies
                            .Include(m => m.MovieGenres)
                            .ThenInclude(mg => mg.Genre)
                            .FirstOrDefaultAsync(m => m.ImdbId == movieApiResponse.imdbid);

                        if (existingMovie != null)
                        {
                            // Film mevcutsa güncelle
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
                            existingMovie.MovieGenres.Clear();
                            if (movieApiResponse.genre != null)
                            {
                                foreach (var genreName in movieApiResponse.genre.Distinct()) // Türleri benzersiz yap
                                {
                                    // Türün var olup olmadığını kontrol et
                                    var genre = await _context.Genres
                                        .FirstOrDefaultAsync(g => g.GenreName == genreName)
                                        ?? new Genre { GenreName = genreName };

                                    // Türü mevcut değilse ekle
                                    if (!await _context.Genres.AnyAsync(g => g.GenreName == genre.GenreName))
                                    {
                                        _context.Genres.Add(genre);
                                        await _context.SaveChangesAsync();
                                    }

                                    // Film ile türü ilişkilendir
                                    existingMovie.MovieGenres.Add(new MovieGenre { Movie = existingMovie, Genre = genre });
                                }
                            }
                        }
                        else
                        {
                            // Mevcut değilse yeni bir film ekle
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
                                MovieGenres = new List<MovieGenre>() // Türler burada eklenecek
                            };

                            // Türleri ekleyin (benzersiz şekilde)
                            if (movieApiResponse.genre != null)
                            {
                                foreach (var genreName in movieApiResponse.genre.Distinct()) // Benzersiz türleri ekle
                                {
                                    var genre = await _context.Genres
                                        .FirstOrDefaultAsync(g => g.GenreName == genreName)
                                        ?? new Genre { GenreName = genreName };

                                    // Türü ekle (varsa mevcut olanı kullan)
                                    if (!await _context.Genres.AnyAsync(g => g.GenreName == genre.GenreName))
                                    {
                                        _context.Genres.Add(genre);
                                        await _context.SaveChangesAsync();
                                    }

                                    movie.MovieGenres.Add(new MovieGenre { Movie = movie, Genre = genre });
                                }
                            }

                            _context.Movies.Add(movie);
                        }
                    }

                    // Değişiklikleri veritabanına kaydet
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

    //API'den çekilen veriler için model sınıfı
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
