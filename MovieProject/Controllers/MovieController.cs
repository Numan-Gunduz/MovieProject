using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Context;
using MovieProject.Models;
using MovieProject.Services;

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
        public async Task<IActionResult> Fetch()
        {
            await _movieService.FetchAndSaveMoviesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var movies = _context.Movies.Include(m => m.Genres).ToList();
            return View(movies);
        }
        // GET: Movies
     

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie, int[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                if (selectedGenres != null)
                {
                    movie.Genres = _context.Genres.Where(g => selectedGenres.Contains(g.GenreId)).ToList();
                }
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genres).FirstOrDefault(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Genres = _context.Genres.ToList();
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
                if (selectedGenres != null)
                {
                    movie.Genres = _context.Genres.Where(g => selectedGenres.Contains(g.GenreId)).ToList();
                }
                _context.Update(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Include(m => m.Genres).FirstOrDefault(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
