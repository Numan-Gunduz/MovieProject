using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Context;
using MovieProject.Models;
using MovieProject.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        private readonly MovieService _movieService;

        public MovieController(MovieContext context, MovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }
        //public IActionResult Genres()
        //{
        //    var genres = _context.Genres.ToList(); // Tüm türleri getiriyoruz
        //    return View(genres); // Genre'leri gösterecek bir View
        //}

        // Belirli bir türe göre filmleri listeleyen action
        public IActionResult MoviesByGenre(int genreId)
        {
            // Seçilen türe göre filmleri getir
            var movies = _context.MovieGenres
                .Where(mg => mg.GenreId == genreId)
                .Include(mg => mg.Movie)
                .Select(mg => mg.Movie)
                .ToList();

            // ViewBag ile tür ismini view'a gönderiyoruz
            ViewBag.Genre = _context.Genres.FirstOrDefault(g => g.GenreId == genreId)?.GenreName;
            return View(movies); // Filmler MoviesByGenre view'ına gönderilecek
        }
        public async Task<IActionResult> FetchMovies()
        {
            await _movieService.FetchAndSaveMoviesAsync();
            return RedirectToAction("Index");
    }
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .ToListAsync();

            ViewBag.Genres = await _context.Genres.ToListAsync(); // Türleri getiriyoruz
            return View(movies);
        }

        //public IActionResult MoviesByGenre()
        //{
        //    var genresWithMovies = _movieService.GetMoviesByGenres();
        //    return View(genresWithMovies);
        //}

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, int[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                if (selectedGenres != null && selectedGenres.Length > 0)
                {
                    foreach (var genreId in selectedGenres)
                    {
                        var movieGenre = new MovieGenre
                        {
                            Movie = movie,
                            GenreId = genreId
                        };
                        _context.MovieGenres.Add(movieGenre);
                    }
                }
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = await _context.Genres.ToListAsync();
            return View(movie);
        }

        // GET: Movies/Edit/5
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Türlerin listesini al
            movie.MovieGenres = await _context.MovieGenres
                .Where(mg => mg.MovieId == movie.MovieID)
                .Include(mg => mg.Genre)
                .ToListAsync();

            return View(movie);
        }


        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie movie, int[] selectedGenres)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Seçilen türleri filmle ilişkilendiriyoruz
                movie.MovieGenres = selectedGenres?.Select(genreId => new MovieGenre { GenreId = genreId }).ToList() ?? new List<MovieGenre>();

                _context.Update(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Genres = _context.Genres.ToList();
            // Hatalı durumda seçilen türleri tekrar ekliyoruz
            ViewBag.SelectedGenres = selectedGenres?.ToList() ?? new List<int>();
            return View(movie);
        }


        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.MovieID == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
